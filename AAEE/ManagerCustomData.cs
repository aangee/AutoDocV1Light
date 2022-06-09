using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {
        public class ManagerCustomData
        {

            Program prgm;
            string tagName;
            string templateOption;

            public ManagerCustomData(Program _prgm)
            {
                this.prgm = _prgm;
                this.tagName = _prgm.tagName;

                //Template pour options utilisateur
                this.templateOption = this.tagName + "\n" +
                "NameConnector = Connector\n" +
                "NameCockpit = Cockpit\n" +
                "TimerDeco = 10";
            }

            internal void InitCustomData()
            {
                string customData = this.prgm.managerBlock.cockpitShip.CustomData;

                if (!customData.Contains(this.tagName)) {

                    this.prgm.managerBlock.cockpitShip.CustomData = templateOption;
                }
                string firstLigne = customData.Split('\n')[0];

                //On decoupe apres chaque nouvelle ligne
                string[] setupData = customData.Split('\n');
                //On decoupe encore apres chaque '=' tous nos lignes
                string[] nameConnector = setupData[1].Split('=');
                string[] nameCockpit = setupData[2].Split('=');
                string[] timerDeconnection = setupData[3].Split('=');

                //Verification si les options son valide
                bool isValideNameConnector = setupData[1].Contains(" = ");
                bool isValideNameCockpit = setupData[2].Contains(" = ");
                bool isValideTimerDeconnection = setupData[3].Contains(" = ");

                //On decoupe une derniere foi pour avoir vraiment que le nom donne en param par l'utilisateur
                string justeNameConnector = nameConnector.Last().Trim();
                string justeNameCockpit = nameCockpit.Last().Trim();
                string justeStrTimer = timerDeconnection.Last().Trim();

                //Verification que le param pour le timer soi un 'int'.
                //FIXME: HardCode si se n'ai pas le cas on mets sur '5' 
                int justeIntTimer = (int.TryParse(justeStrTimer, out justeIntTimer))? justeIntTimer : 5 ;
                





                if (this.prgm.isActiveDebugInProgramBloc)
                {
                    this.prgm.Echo("Params cusdomData:"); 

                    this.prgm.Echo("Name connector valide: " + isValideNameConnector);
                    this.prgm.Echo("Name cockpit valide: " + isValideNameCockpit);
                    this.prgm.Echo("Timer valide: " + isValideTimerDeconnection);

                    this.prgm.Echo(justeNameConnector);
                    this.prgm.Echo(justeNameCockpit);
                    this.prgm.Echo(justeIntTimer.ToString());
                }
            }
        }
    }
}
