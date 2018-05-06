using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Client
{
    public class Position
    {
        public int PosX { get; set; }

        public int PosY { get; set; }

        /// <summary>
        /// ueberschreibe Base.ToString.  Für Log-Informationen zu erhalten
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strLogPosition = "Pos X = " + PosX.ToString()+ ", Pos Y = " +  PosY.ToString();

            return strLogPosition;
        }

        /// <summary>
        /// Kopieren alle Werte der Property für Möglichkeit zum vergleichen (Aktuell--> Vorheriges).
        /// </summary>
        /// <returns></returns>
        public Position ShallowCopy()
        {
            return (Position)this.MemberwiseClone();
        }

    }
}
