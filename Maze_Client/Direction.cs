using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Client
{
    public enum MoveDirection
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        None = 4
    }

    public class Direction
    {
        public Boolean North { get; set; }

        public Boolean East { get; set; }

        public Boolean South { get; set; }

        public Boolean West { get; set; }


        // ueberschreibe Base.ToString.  Für Log-Informationen zu erhalten
        public override string ToString()
        {
            string strLogDirections = "North=" + North.ToString() + ", East=" + East.ToString() + ", South=" + South.ToString() + ", West=" + West.ToString();

            return strLogDirections;
        }              

        /// <summary>
        /// Kopieren alle Werte der Property für Möglichkeit zum vergleichen (Aktuell--> Vorheriges).
        /// </summary>
        /// <returns></returns>
        public Direction ShallowCopy()
        {
            return (Direction)this.MemberwiseClone();
        }

        /// <summary>
        /// GetPropertiesNameOfClass. Erstellt eine Liste mit allen Property-Namen
        /// </summary>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public List<string> GetPropertiesNameOfClass(object pObject)
        {
            List<string> propertyList = new List<string>();
            if (pObject != null)
            {
                foreach (var prop in pObject.GetType().GetProperties())
                {
                    propertyList.Add(prop.Name);
                }
            }
            return propertyList;
        }

        /// <summary>
        /// GetValue.  Gibt Wert zurück. Aufgerufen mit einem Enum
        /// </summary>
        /// <param name="_moveDirection"></param>
        /// <returns></returns>
        public Boolean GetValue(MoveDirection _moveDirection)
        {
            Boolean lbValue = false;
           
            switch (_moveDirection)
            {
                case MoveDirection.North:
                    lbValue = this.North;                   
                    break;
                case MoveDirection.East:
                    lbValue = this.East;
                    break;
                case MoveDirection.South:
                    lbValue = this.South;
                    break;
                case MoveDirection.West:
                    lbValue = this.West;
                    break;
                default:
                    break;
            }
            return lbValue;
        }       
    }
}
