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

                this.templateOption = this.tagName + "\n" +
                "NameCockpit = Cockpit\n" +
                "NameConnector = Connector\n" +
                "TimerDeco = 10";
            }

            internal void InitCustomData()
            {
                string customData = this.prgm.managerBlock.cockpitShip.CustomData;

                if (!customData.Contains(this.tagName)) {

                    this.prgm.managerBlock.cockpitShip.CustomData = templateOption;
                }
                string firstLigne = customData.Split('\n')[0];

                string[] setupData = customData.Split('\n');
                string[] nameConnector = setupData[1].Split('=');
                string[] nameCockpit = setupData[2].Split('=');
                string[] timerDeconnection = setupData[3].Split('=');

                bool isValideNameConnector = nameConnector.Contains(" = ");
                bool isValideNameCockpit = nameCockpit.Contains(" = ");
                bool isValideTimerDeconnection = timerDeconnection.Contains(" = ");


                string justeNameConnector = nameConnector.Last().Trim();
                string justeNameCockpit = nameCockpit.Last().Trim();
                string justeStrTimer = timerDeconnection.Last().Trim();

                int justeIntTimer = (int.TryParse(justeStrTimer, out justeIntTimer))? justeIntTimer : 5 ;
                





                if (this.prgm.isActiveDebugInProgramBloc) this.prgm.Echo(justeIntTimer.ToString());
            }
        }
    }
}
