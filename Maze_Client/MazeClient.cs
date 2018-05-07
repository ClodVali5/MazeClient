using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Maze_Client
{
    public partial class MazeClient : Form
    {
        public string mcState { get; set; }

        public int mnPosX { get; set; }
        public int mnPosY { get; set; }

        Position moPosition = null;    

        public Direction moDirections = new Direction();
      
        public int mnMaxDirections { get; set; }

        public List<string> mlcAllDirections = new List<string>();

        public List<string> mlcResponseList = new List<string>();

        public List<Position> mloPosition = new List<Position>();

        /// <summary>
        /// Init the Form
        /// </summary>
        public MazeClient()
        {
            InitializeComponent();

            LaunchMazeServer();

            mlcAllDirections = moDirections.GetPropertiesNameOfClass(moDirections);

            mcState = GetState();
            GetPosition();
            GetDirections();

            txtMaxDirections.Text = mnMaxDirections.ToString();

            //ThreadStart solveref = new ThreadStart(CallToSolveThread);
            //Thread solveThread = new Thread(solveref);
        }

        private void CallToSolveThread()
        {
            SolveLeftHand(MoveDirection.East);           
        }

        /// <summary>
        /// GetState
        /// </summary>
        /// <returns></returns>
        public string GetState()
        {
            string lcState = null;
            string lcResponse = null;

            // neuen Status abfragen
            lcResponse = RequestGET(Constants.RequestURL + "/state");

            mlcResponseList.Clear();
            mlcResponseList = ConvertToObject(lcResponse);

            if (mlcResponseList.Contains("State"))
            {
                var index = mlcResponseList.FindIndex(item => item == "State");

                lcState = mlcResponseList[index + 1];
                mcState = lcState;
            }

            if (this.txtStateRun.InvokeRequired)
            {
                this.txtStateRun.Invoke((MethodInvoker)delegate ()
                {
                    this.txtStateRun.Text = mcState;
                }
                );
            }
            else
            {
                this.txtStateRun.Text = mcState;
            }
                        
            return lcState;
        }

        /// <summary>
        /// GetPosition
        /// </summary>
        private void GetPosition()
        {
            string cResponse = null;

            // neue Position abfragen
            cResponse = RequestGET(Constants.RequestURL + "/position");

            mlcResponseList.Clear();
            mlcResponseList = ConvertToObject(cResponse);

            if (mlcResponseList.Contains("Position"))
            {
                int index = -1;
                index = mlcResponseList.FindIndex(item => item == "X");
                mnPosX = Int32.Parse(mlcResponseList[index + 1]);

                index = mlcResponseList.FindIndex(item => item == "Y");
                mnPosY = Int32.Parse(mlcResponseList[index + 1]);
            }

            moPosition = new Position { PosX = mnPosX, PosY = mnPosY };
        }

        /// <summary>
        /// GetDirections
        /// </summary>
        public void GetDirections()
        {            
            string cResponse = null;

            // neue Richtungen (Directions) abfragen
            cResponse = RequestGET(Constants.RequestURL + "/directions");

            mlcResponseList.Clear();
            mlcResponseList = ConvertToObject(cResponse);

            if (mlcResponseList.Contains("North") || mlcResponseList.Contains("East") || mlcResponseList.Contains("South") || mlcResponseList.Contains("West"))
            {
                int index = -1;

                index = mlcResponseList.FindIndex(item => item == "North");
                moDirections.North = Boolean.Parse(mlcResponseList[index + 1]);

                index = mlcResponseList.FindIndex(item => item == "East");
                moDirections.East = Boolean.Parse(mlcResponseList[index + 1]);

                index = mlcResponseList.FindIndex(item => item == "South");
                moDirections.South = Boolean.Parse(mlcResponseList[index + 1]);

                index = mlcResponseList.FindIndex(item => item == "West");
                moDirections.West = Boolean.Parse(mlcResponseList[index + 1]);

                // Anzahl Wege, Richtungen ausfinden
                mnMaxDirections = GetMaxDirections(mlcResponseList);

                // Log  aktuelle Richtungen
                MoveLogStep();
            }
        }

        /// <summary>
        /// GetMaxDirections
        /// </summary>
        /// <param name="_listResponseList"></param>
        /// <returns></returns>
        public int GetMaxDirections(List<string> _listResponseList)
        {
            int nMaxDirections = 0;

            foreach (var item in _listResponseList)
            {
                if (item.Contains("True"))
                {
                    nMaxDirections = nMaxDirections + 1;
                }
            }
                        
            return nMaxDirections;
        }

        /// <summary>
        /// GetReset
        /// </summary>
        public void GetReset()
        {
            // Werte, Variablen zurücksetzen
            txtStatus.Text = string.Empty;
            txtMovement.Text = String.Empty;
            mloPosition.Clear();

            // Reset ausführen
            string cRequestUri = Constants.RequestURL + "/Reset";
            string cPostData = txtPostData.Text;
                      
            txtStatus.Text = RequestPOST(cPostData, cRequestUri);        

            mcState = GetState();
            GetPosition();
            GetDirections();
        }

        /// <summary>
        /// Starte Maze-Server 
        /// </summary>
        public void LaunchMazeServer()
        {
            Launch StarteMaze = new Launch();
            StarteMaze.LaunchMazeServer(Constants.fn, "");
        }       

        /// <summary>
        /// SolveLeftHand. Solve the Maze-Labyrinth
        /// </summary>
        /// <param name="_moveDirection"></param>
        /// <returns></returns>
        public Boolean SolveLeftHand(MoveDirection _moveDirection)
        {
            MoveDirection loInFront = _moveDirection;
            MoveDirection loLeftSide = RotateDirection(loInFront, false);        
            Boolean lbWallNotInFront = moDirections.GetValue(loInFront);
            Boolean lbWallNotLefSide = moDirections.GetValue(loLeftSide);     
            Boolean lbPositonExit = false;            

            // Have we reached the end ? ::1
            if (mcState.Contains("Target"))
            {
                return false;
            }

            // Special Conditions
            if (mnMaxDirections > 2)
            {
                Console.WriteLine($"Kommt aus welcher Richtung ? --> {_moveDirection} ");

                // chek all Directions
                foreach (var item in mlcAllDirections)
                {
                    var cDirectionTyp = item.ToString();
                    MoveDirection oDirectionObject = MoveDirection.None;
                    Enum.TryParse<MoveDirection>(cDirectionTyp, out oDirectionObject);
                    bool lbIsFree = WayIsFree(oDirectionObject);
                    bool lbIsMark = PositionIsMark(moPosition, oDirectionObject);

                    Console.WriteLine($"Richtung  =   {oDirectionObject}   Frei = {lbIsFree}   Bekannkt = {lbIsMark} ");

                    //// Whenn is don't mark go 
                    if (lbIsFree && !lbIsMark && (oDirectionObject == loInFront))
                    {
                        //Step forward
                        MoveMaze(loInFront);

                        // Recalls method 
                        SolveLeftHand(loInFront);
                    }

                    if (lbIsFree && !lbIsMark)
                    {
                        //Step forward
                        MoveMaze(oDirectionObject);

                        // Recalls method 
                        SolveLeftHand(oDirectionObject);
                    }
                }
                Console.WriteLine($"Alle Richtungen untersucht:      !!! ");
            }

            // There is no Left wall  ::2
            if (lbWallNotLefSide == true)  
            {              
                // Counter Clockwise 90 deg(CCW)  West --> CCW = South
                loLeftSide = RotateDirection(loInFront, false);
                
                //Step forward
                MoveMaze(loLeftSide);

                // Recalls method 
                SolveLeftHand(loLeftSide);
            }

            // There is no front wall  :3
            if (lbWallNotInFront == true)
            {
                // ueberprüfen ob schon gewesen           
                lbPositonExit = mloPosition.Exists(x => x.PosX == mnPosX && x.PosY == mnPosY);
                
                if (!lbPositonExit)
                {
                    mloPosition.Add(new Position { PosX = mnPosX, PosY = mnPosY });
                }             

                //Step forward
                MoveMaze(loInFront);

                // Recalls method 
                SolveLeftHand(loInFront);
            }
            else
            {
                // Rotate CW 90 deg
                loInFront = RotateDirection(loInFront, true);

                // Recalls method 
                SolveLeftHand(loInFront);
            }

            return false;

        }

        /// <summary>
        /// SolveRightHand
        /// </summary>
        /// <param name="_moveDirection"></param>
        /// <returns></returns>
        public Boolean SolveRightHand(MoveDirection _moveDirection)
        {          
                MoveDirection loInFront = _moveDirection;
                ///MoveDirection loLeftSide = RotateDirection(loInFront, false);
                MoveDirection LoRightSide = RotateDirection(loInFront, true);
                Boolean lbWallNotInFront = moDirections.GetValue(loInFront);
              
                Boolean lbWallNotRighSide = moDirections.GetValue(LoRightSide);
                Boolean lbPositonExit = false;

               
            // Have we reached the end ?   ::1
            if (mcState.Contains("Target"))
            {
                return false;            }
            
            
            // There is not right wall   ::2
            if (lbWallNotRighSide == true)
            {
                // ueberprüfen ob schon gewesen           
                lbPositonExit = mloPosition.Exists(x => x.PosX == mnPosX && x.PosY == mnPosY);

                if (!lbPositonExit)
                {
                    mloPosition.Add(new Position { PosX = mnPosX, PosY = mnPosY });
                }              

                // Counter Clockwise 90 deg(CCW)  West --> CCW = South
                LoRightSide = RotateDirection(loInFront, true);

                //Step forward
                MoveMaze(LoRightSide);

                SolveRightHand(LoRightSide);               
            }


            // There is no front wall   ::3
            if (lbWallNotInFront == true)
            {
                // ueberprüfen ob schon gewesen           
                lbPositonExit = mloPosition.Exists(x => x.PosX == mnPosX && x.PosY == mnPosY);

                if (!lbPositonExit)
                {
                    var test = true;
                    mloPosition.Add(new Position { PosX = mnPosX, PosY = mnPosY });
                }
                else
                {
                    var test = true;

                    // Change the Search-Method --> new SolveLeftHand
                    LoRightSide = RotateDirection(LoRightSide, false);
                    Console.WriteLine("neu SolveLeftHand");

                    //Step forward
                    MoveMaze(LoRightSide);

                    SolveLeftHand(LoRightSide);

                }

                //Step forward
                MoveMaze(loInFront);

                SolveRightHand(loInFront);
            }
            else
            {
                // Rotate CW 90 deg
                loInFront = RotateDirection(loInFront, false);

                SolveRightHand(loInFront);
            }

            return false;
        }

        /// <summary>
        /// RotateDirection
        /// </summary>
        /// <param name="_moveDirection"></param>
        /// <param name="_clockWise"></param>
        /// <returns></returns>
        private MoveDirection RotateDirection(MoveDirection _moveDirection, Boolean _clockWise)
        {
            // Clockwise 90 deg = true ,  Counter Clock Wise 90 deg = false

            switch (_moveDirection)
            {
                case MoveDirection.North:
                    _moveDirection = _moveDirection = _clockWise ?  MoveDirection.East : MoveDirection.West;
                    break;
                case MoveDirection.East:
                    _moveDirection = _moveDirection = _clockWise ? MoveDirection.South : MoveDirection.North;
                    break;
                case MoveDirection.South:
                    _moveDirection = _moveDirection = _clockWise ? MoveDirection.West : MoveDirection.East;
                    break;
                case MoveDirection.West:
                    _moveDirection = _moveDirection = _clockWise ? MoveDirection.North : MoveDirection.South;
                    break;
                default:                    
                    break;
            }

            return _moveDirection;
        }

        /// <summary>
        /// WayIsFree.
        /// </summary>
        /// <param name="_moveDirection"></param>
        /// <returns></returns>
        private Boolean WayIsFree(MoveDirection _moveDirection)
        {
            MoveDirection oMoveDirection = _moveDirection;
            Boolean bWayFree = false;

            // Maze steht vor einer Wand
            switch (oMoveDirection)
            {
                case MoveDirection.North:
                    bWayFree = moDirections.North == true ? bWayFree = true : bWayFree = false;
                    break;
                case MoveDirection.East:
                    bWayFree = moDirections.East == true ? bWayFree = true : bWayFree = false;
                    break;
                case MoveDirection.South:
                    bWayFree = moDirections.South == true ? bWayFree = true : bWayFree = false;
                    break;
                case MoveDirection.West:
                    bWayFree = moDirections.West == true ? bWayFree = true : bWayFree = false;
                    break;
                default:
                    bWayFree = false;
                    break;
            }

            return bWayFree;
        }

        /// <summary>
        /// PositionIsMark. Is the Direction in Front, already gone through 
        /// </summary>
        /// <param name="_actualPosition"></param>
        /// <param name="_moveDirection"></param>
        /// <returns></returns>
        private Boolean PositionIsMark(Position _actualPosition, MoveDirection _moveDirection)
        {           
            Position loPosition = _actualPosition.ShallowCopy();
            MoveDirection loMoveDirection = _moveDirection;
          
            Position Front = loPosition;
            //Position Back = loposition;
            //Position Left = loposition;
            //Position Right = loposition;

            Boolean lbPositonExit = false;
            Boolean bWayMark = false;

            // Position der kommenende Richtung in Front kontrollieren ob bekannt (markiert)
            switch (loMoveDirection)
            {
                case MoveDirection.North:                   

                    Front.PosX = loPosition.PosX;
                    Front.PosY = loPosition.PosY - 1;

                    lbPositonExit = mloPosition.Exists(x => x.PosX == Front.PosX && x.PosY == Front.PosY);

                    break;
                case MoveDirection.East:
                  
                    Front.PosX = loPosition.PosX + 1;
                    Front.PosY = loPosition.PosY;

                    lbPositonExit = mloPosition.Exists(x => x.PosX == Front.PosX && x.PosY == Front.PosY);
               
                    break;
                case MoveDirection.South:

                    Front.PosX = loPosition.PosX;
                    Front.PosY = loPosition.PosY + 1;

                    lbPositonExit = mloPosition.Exists(x => x.PosX == Front.PosX && x.PosY == Front.PosY);
                
                    break;
                case MoveDirection.West:

                    Front.PosX = loPosition.PosX - 1;
                    Front.PosY = loPosition.PosY;

                    lbPositonExit = mloPosition.Exists(x => x.PosX == Front.PosX && x.PosY == Front.PosY);                  

                    break;
                default:

                    Front.PosX = loPosition.PosX;
                    Front.PosY = loPosition.PosY;
                   
                    break;
            }

            Console.WriteLine($"Richtung -->  {loMoveDirection}   Position = ( {loPosition.PosX}, {loPosition.PosY} ) ");

            if (lbPositonExit)
            {
                return bWayMark = true ;
            }
            else
            {
                return bWayMark = false;
            }          
        }


        /// <summary>
        /// cmdStart_Click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdStart_Click(object sender, EventArgs e)
        {
            GetReset();

            ThreadStart solveref = new ThreadStart(CallToSolveThread);
            Thread solveThread = new Thread(solveref);
            solveThread.Start();


            // SolveLeftHand(MoveDirection.East);
            //   SolveRightHand(MoveDirection.East);           
        }

        /// <summary>
        /// cmdStop_Click. Event from Button cmdStop "Stop" 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdStop_Click(object sender, EventArgs e)
        {

        }
        
        /// <summary>
        /// cmdSend_Click. Event from Button cmdSend "Send"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSend_Click(object sender, EventArgs e)
        {
            string lcRequestUri = null;
            string lcMethod = null;
            string lcPostData = null;
            string cResponseData = null;

            lcRequestUri = Constants.RequestURL + txtMessage.Text;
            lcMethod = chkPOST.Checked ? lcMethod = "POST" : lcMethod = "GET";
            lcPostData = txtPostData.Text;

            //// !  TEST: Json schreiben !
            //StringBuilder sb = new StringBuilder();
            //StringWriter sw = new StringWriter(sb);
            //using (JsonWriter writer = new JsonTextWriter(sw))
            //{
            //    writer.Formatting = Formatting.Indented;

            //    writer.WriteStartObject();
            //    writer.WritePropertyName("South");
            //    writer.WriteValue(true);
            //    writer.WriteEndObject();
            //}
            //// !

            // Execute  POST or GET  -  Request
            if (chkPOST.Checked)
            {
                cResponseData = RequestPOST(lcPostData, lcRequestUri);               
            }
            else
            {
                cResponseData = RequestGET(lcRequestUri);

                mlcResponseList.Clear();
                mlcResponseList = ConvertToObject(cResponseData);
            }

            txtStatus.Text += cResponseData + Environment.NewLine;          
        }        

        /// <summary>
        ///  Request für HTTP Post  
        /// </summary>
        /// <param name="ClientData">ClientData = It's the data we want to pass to the URL</param>
        /// <param name="PostURL">PostURL = The actual URL we need to pass the data to</param>
        /// <returns></returns>       
        public string RequestPOST(string ClientData, string PostURL)
        {
            string lcResponseFromServer = string.Empty;

            try
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(PostURL);

                // Set the Method property of the request to POST.
                request.Method = "POST";

                // Create POST data and convert it to a byte array.
                byte[] byteArray = Encoding.UTF8.GetBytes(ClientData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/json";
                ///request.ContentType = "application/x-www-form-urlencoded";

                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;

                // Get the request stream.
                Stream dataStream = request.GetRequestStream();

                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                WebResponse response = request.GetResponse();

                if (((HttpWebResponse)response).StatusDescription == "OK")
                {
                    // Get the stream containing content returned by the server.
                    dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    lcResponseFromServer = reader.ReadToEnd();

                    // Clean up the streams.
                    reader.Close();
                    dataStream.Close();
                }

                response.Close();

                //ToDo  Aus Int den enum.Name rausfinden
                
                             
                // LOG: Aufzeichnung wenn ein Reset ausgeführt wird. Keine Rueckmeldung vom Server
                if (PostURL.ToLower().Contains("reset")) { lcResponseFromServer += ".LOG: Es wurde ein Reset ausgeführt" + Environment.NewLine; }
                if (PostURL.ToLower().Contains("move")) { lcResponseFromServer += $".LOG: Bewegung ausgefuerht = {ClientData} "; }

                return lcResponseFromServer;
            }
            catch (Exception ex)
            {
                string error = "Err: " + ex.Message;
                lcResponseFromServer = error;

                return lcResponseFromServer;
            }
        }

        /// <summary>
        ///   Request für HTTP Get  
        /// </summary>
        /// <param name="requestUri">requestUri = The actual URL we need)</param>
        /// <returns></returns>       
        private string RequestGET(string requestUri)
        {
            string lcResponseFromServer = string.Empty;
            try
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(requestUri);

                // Set the Method property of the request to POST.
                request.Method = "GET";

                // Create POST data and convert it to a byte array.
                byte[] byteArray = Encoding.UTF8.GetBytes(string.Empty);

                Stream dataStream;
                WebResponse response = request.GetResponse();

                if (((HttpWebResponse)response).StatusDescription == "OK")
                {
                    // Get the stream containing content returned by the server.
                    dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    lcResponseFromServer = reader.ReadToEnd();
                    // Clean up the streams.
                    reader.Close();
                    dataStream.Close();
                }

                response.Close();

                return lcResponseFromServer;
            }
            catch (Exception ex)
            {
                lcResponseFromServer = "Err: " + ex.Message;
                return lcResponseFromServer;
            }
        }

        /// <summary>
        /// Konvertiere die ResponseDaten (Member)
        /// </summary>
        /// <param name="_response"></param>
        private List<string> ConvertToObject(string _response)
        {
            string lcResponse = _response;
            List<string> llResponseList = new List<string>();

            // Response umwandlen 
            if (!_response.Contains("Err"))
            {
                JsonTextReader reader = new JsonTextReader(new StringReader(lcResponse));
                while (reader.Read())
                {
                    if (reader.Value != null)
                    {
                        llResponseList.Add(reader.Value.ToString());
                    }
                    else
                    {
                        /// ToDo  Debug   :: Console.WriteLine("Token: {0}", reader.TokenType);
                    }
                }
            }

            return llResponseList;
        }


        /// <summary>
        /// MoveLogStep. Log-Information over the Step
        /// </summary>
        public void MoveLogStep()
        {
            string lcMoveLog = null;

            lcMoveLog = "Pos: X=" + mnPosX + ", Y=" + mnPosY + " // State: " + mcState + " // Dir: " + moDirections.ToString();
                         
            if (this.txtMovement.InvokeRequired)
            {
                this.txtMovement.Invoke((MethodInvoker)delegate ()
                {
                    this.txtMovement.Text = lcMoveLog;
                }
                );
            }
            else
            {
                this.txtMovement.Text = lcMoveLog;
            } 
        }


        /// <summary>
        /// MoveOneStep. 
        /// </summary>
        public void MoveOneStep()
        {
            // Bewegung ausführen
            string cRequestUri = Constants.RequestURL + "/move";
            string cPostData = txtPostData.Text;  // Wert aus Textfeld
            string cResponseData = null;

            cResponseData = RequestPOST(cPostData, cRequestUri);

            // Log Informationen
            txtStatus.Text += cResponseData + Environment.NewLine;

            // Info sammeln
            GetState();
            GetPosition();

            GetDirections();

        }

        /// <summary>
        /// MoveMaze. One Step forward
        /// </summary>
        /// <param name="_moveDirection"></param>
        public void MoveMaze(MoveDirection _moveDirection)
        {
            // Bewegung ausführen
            string cRequestUri = Constants.RequestURL + "/move";
            string cPostData = ((int)_moveDirection).ToString();  
            string cResponseData = null;

            cResponseData = RequestPOST(cPostData, cRequestUri);

            // Log Informationen  ToDo
            ///Thread ::    txtStatus.Text += cResponseData + Environment.NewLine;

            // Info sammeln
            GetState();
            GetPosition();
            GetDirections();

        }

        #region MOVE MANUAL
    
        /// <summary>
        /// MoveNorth. Move one Step up (manuel)
        /// </summary>
        public void MoveNorth()
        {
            // Bewegung ausführen
            string cRequestUri = Constants.RequestURL + "/move";
            string cPostData = "0";  // Wert aus Textfeld
            string cResponseData = null;

            cResponseData = RequestPOST(cPostData, cRequestUri);

            // Log Informationen
            txtStatus.Text += cResponseData + Environment.NewLine;

            // Info sammeln
            GetState();
            GetPosition();
            GetDirections();
        }

        /// <summary>
        /// MoveWest. Move one Step to the left (manual)
        /// </summary>
        public void MoveWest()
        {
            // Bewegung ausführen
            string cRequestUri = Constants.RequestURL + "/move";
            string cPostData = "3";  // Wert aus Textfeld
            string cResponseData = null;

            cResponseData = RequestPOST(cPostData, cRequestUri);

            // Log Informationen
            txtStatus.Text += cResponseData + Environment.NewLine;

            // Info sammeln
            GetState();
            GetPosition();
            GetDirections();
        }

        /// <summary>
        /// MoveSouth. Move one Step down (manual)
        /// </summary>
        public void MoveSouth()
        {
            // Bewegung ausführen
            string cRequestUri = Constants.RequestURL + "/move";
            string cPostData = "2";  // Wert aus Textfeld
            string cResponseData = null;

            cResponseData = RequestPOST(cPostData, cRequestUri);

            // Log Informationen
            txtStatus.Text += cResponseData + Environment.NewLine;

            // Info sammeln
            GetState();
            GetPosition();
            GetDirections();
        }

        /// <summary>
        /// MoveEast. Move one Step to the right (manual)
        /// </summary>
        public void MoveEast()
        {
            // Bewegung ausführen
            string cRequestUri = Constants.RequestURL + "/move";
            //string cPostData =  "1";  
            string cPostData = ((int)MoveDirection.East).ToString();

            string cResponseData = null;                       

            cResponseData = RequestPOST(cPostData, cRequestUri);

            // Log Informationen
            txtStatus.Text += cResponseData + Environment.NewLine;

            // Info sammeln
            GetState();
            GetPosition();
            GetDirections();
        }

        #endregion MOVE MANUAL

        /// <summary>
        /// ReverseDirection. 
        /// </summary>
        /// <param name="_moveDirection"></param>
        /// <returns></returns>
        private MoveDirection ReverseDirection(MoveDirection _moveDirection)
        {
            // Richtungswechseln.  z.B.  North --> South
            switch (_moveDirection)
            {
                case MoveDirection.North:
                    _moveDirection = MoveDirection.South;
                    break;
                case MoveDirection.East:
                    _moveDirection = MoveDirection.West;
                    break;
                case MoveDirection.South:
                    _moveDirection = MoveDirection.North;
                    break;
                case MoveDirection.West:
                    _moveDirection = MoveDirection.East;
                    break;
                default:
                    break;
            }

            return _moveDirection;
        }

        /// <summary>
        /// txtStatus_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtStatus_TextChanged(object sender, EventArgs e)
        {
            txtStatus.SelectionStart = txtStatus.Text.Length;
            txtStatus.ScrollToCaret();
        }

        /// <summary>
        /// txtMovement_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMovement_TextChanged(object sender, EventArgs e)
        {
            txtMovement.SelectionStart = txtMovement.Text.Length;
            txtMovement.ScrollToCaret();
        }

        #region DEBUG
        private void debugOutput(string _debugText)
        {
            try
            {
                System.Diagnostics.Debug.Write(_debugText + Environment.NewLine);
                txtStatus.Text = txtStatus.Text + _debugText + Environment.NewLine;
                txtStatus.SelectionStart = txtStatus.TextLength;
                txtStatus.ScrollToCaret();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message, ToString() + Environment.NewLine);

            }
        }

        #endregion

        private void cmdReset_Click(object sender, EventArgs e)
        {
            GetReset();
        }

        private void cmdNorth_Click(object sender, EventArgs e)
        {
            if (!(mcState.Contains("Target") || mcState.Contains("Failed"))) 
            {
                MoveNorth();
            }
        }


        private void cmdWest_Click(object sender, EventArgs e)
        {
            if (!(mcState.Contains("Target") || mcState.Contains("Failed")))
            {
                MoveWest();
            }                 
        }

        private void cmdEast_Click(object sender, EventArgs e)
        {
            if (!(mcState.Contains("Target") || mcState.Contains("Failed")))
            {
                MoveEast();
            }          
        }

        private void cmdSouth_Click(object sender, EventArgs e)
        {
            if (!(mcState.Contains("Target") || mcState.Contains("Failed")))
            {
                MoveSouth();
            }           
        }

    }
}
       
