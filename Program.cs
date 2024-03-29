using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        #region Public
        public float timerDeco = 5f;//Une valeur plus haute augment le temps avent deconnection du connecteur
        #endregion

        #region Private
        // Pour le timer de deco du connector
        float timerDeconnection = 0;
        bool isDecoConnector = false;
        //On stock le name de la grid du programBloc
        string uidGridShip;
        //Pour eviter qu'il recompile au restart serveur
        bool isAvoidRestart = false;
        //Pour active le debug 
        bool isActiveDebugInProgramBloc = true;
        bool isActiveFullDebug = false;
        ManagerBlocks managerBlock;
        #endregion

        #region Base program
        public Program() {
            // Réglage frequence update
            Runtime.UpdateFrequency = UpdateFrequency.Update10;
            if (isAvoidRestart == false)
            {
                //On recup le name(uniqueID) de la grid ou se trouve le programmeBlock
                uidGridShip = Me.CubeGrid.Name;
                //Avec sa ont gere tous les bloc du ship
                managerBlock = new ManagerBlocks(this);
            }
        }

        public void Main(string argument, UpdateType updateSource) {
            if (managerBlock.connectorShip != null)  Update_P(argument);
        }
        #endregion

        void Update_P(string _args) {
            if (_args == "co") Connection(); 
            else if (_args == "deco") {
                isDecoConnector = true; // Pour le retare de deconnection du connecteur
                Deconnection();
            }
            //On lance le timer pour le retard de deconnection du connecteur
            UpdateTimerDeco();
        }

        #region Connection & Deconnection
        /// On check si on peut connecter le connecteur
        /// on désactive les thrusters
        /// et on met en recharge les batteries
        void Connection() {
            // Si on ne peut pas se connecter on ne fait rien
            if (managerBlock.connectorShip.Status == MyShipConnectorStatus.Unconnected) return;
            managerBlock.connectorShip.Connect();
            DeactiveThrusters();
            RestockTanks();
            RechargeBattery();
        }

        /// Ici on active les thrusters
        /// on switch les batteries en auto
        /// et on déconnecte le connecteur
        void Deconnection() {
            if (managerBlock.connectorShip.Status != MyShipConnectorStatus.Connected) return; 
            // Le connector se deco apres un petit temps
            AutoBattery();
            DestockTanks();
            ActiveThrusters();
        }

        /// Timer pour deconnecter, le connecteur
        /// apres avoir allumer les
        /// Thruster, Tank, Batts, Ect
        /// DEFAULT VALUE: 1f
        void UpdateTimerDeco() {
            if (!isDecoConnector) return;

            timerDeconnection += 0.5f;
            if (timerDeconnection >= timerDeco) {
                managerBlock.connectorShip.Disconnect();
                timerDeconnection = 0;
                isDecoConnector = false;
            }
        }
        #endregion

        #region Truster
        void DeactiveThrusters() {
            if (managerBlock.listAllTrusters.Count != 0) 
                foreach (IMyThrust thrusterBlock in managerBlock.listAllTrusters) if (thrusterBlock.Enabled) thrusterBlock.Enabled = false; 
        }
        void ActiveThrusters() {
            if (managerBlock.listAllTrusters.Count != 0) {
                foreach (IMyThrust thrusterBlock in managerBlock.listAllTrusters) {
                    if (!thrusterBlock.Enabled) thrusterBlock.Enabled = true; 
                }
            }
        }
        #endregion

        #region Tank
        void DestockTanks() {
            if (managerBlock.listAllTanks.Count != 0) foreach (IMyGasTank gasBlock in managerBlock.listAllTanks)  gasBlock.Stockpile = false; 
        }
        void RestockTanks() {
            if (managerBlock.listAllTanks.Count != 0) foreach (IMyGasTank gasBlock in managerBlock.listAllTanks) gasBlock.Stockpile = true;  
        }
        #endregion
        
        #region Batt
        /// Ici on met toutes les batteries en recharge
        public void RechargeBattery() {
            if (managerBlock.listAllBatterys.Count != 0) 
                foreach (IMyBatteryBlock batteryBlock in managerBlock.listAllBatterys)  batteryBlock.ChargeMode = ChargeMode.Recharge; 
        }
        /// Ici on met toutes les batteries en auto
        public void AutoBattery() {
            if (managerBlock.listAllBatterys.Count != 0) 
                foreach (IMyBatteryBlock batteryBlock in managerBlock.listAllBatterys) batteryBlock.ChargeMode = ChargeMode.Auto;
        }
        #endregion
    }
}
