using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
       /* AutoTriggerDock
       *  -----------
       *  By Aangee & Walto */
        #region Public

        public ManagerBlocks managBlock;
        public float timerDeco = 5f;//Une valeur plus haute augment le temps avent deconnection du connecteur
        //EDIT: Peut etre mettre une valeur plus comprehensible pour l'utilisateur? 
        //EDIT: Pour les conditions voir pour utiliser "?:" (conditions ternaires, plus compactes et plus lisibles)
        #endregion

        #region Private

        // Pour le timer de deco du connector
        float timerDeconnection = 0;
        bool isDecoConnector = false;

        //On stock le name de la grid du programBloc
        string uidGridShip;

        //Pour active le debug 
        bool isActiveDebugInProgramBloc = true;
        bool isActiveFullDebug = false;

        Debug _DEBUG_OfPrgmBlock;
        #endregion

        #region Base program
        public Program()
        {
            // Réglage frequence update
            Runtime.UpdateFrequency = UpdateFrequency.Update10;

            //On recup le name(uniqueID) de la grid ou se trouve le programmeBlock
            uidGridShip = Me.CubeGrid.Name;

            //Avec sa ont gere tous les bloc du ship
            managBlock = new ManagerBlocks(this);

            //Reglage pour le debug
            InitDebug();
        }

        public void Main(string argument, UpdateType updateSource)
        {
            if (managBlock.connectorShip != null)
            {
                Update_P(argument);
            }
        }
        #endregion

        void Update_P(string _args)
        {
            if (_args == "co")
            {
                Connection();
            }
            else if (_args == "deco")
            {
                isDecoConnector = true;// Pour le retare de deconnection du connecteur
                Deconnection();
            }
            //On lance le timer pour le retard de deconnection du connecteur
            UpdateTimerDeco();

            // Pour mettre a jour le debug
            if (isActiveFullDebug) _DEBUG_OfPrgmBlock.ShowDebug(managBlock); //Debug
        }

        #region Init & Update, debug & ui
        /// <summary>
        /// On cree le debug
        /// </summary>
        void InitDebug()
        {
            //On lance le setup du debug dans le control panel
            if (isActiveFullDebug)
            {
                _DEBUG_OfPrgmBlock = new Debug(this);
                //Echo("Name grid: " + Me.CubeGrid.Name);
                _DEBUG_OfPrgmBlock.ShowDebug(managBlock);

            }

        }
        #endregion

        #region Connection & Deconnection
        /// On check si on peut connecter le connecteur
        /// on désactive les thrusters
        /// et on met en recharge les batteries
        void Connection()
        {
                // Si on ne peut pas se connecter on ne fait rien
                if (managBlock.connectorShip.Status == MyShipConnectorStatus.Unconnected) { return; }

                managBlock.connectorShip.Connect();
                DeactiveThrusters();
                RestockTanks();
                RechargeBattery();
        }

        /// Ici on active les thrusters
        /// on switch les batteries en auto
        /// et on déconnecte le connecteur
        void Deconnection()
        {
            if (managBlock.connectorShip.Status != MyShipConnectorStatus.Connected) { return; }
               
            // Le connector se deco apres un petit temps
            AutoBattery();
            DestockTanks();
            ActiveThrusters();
        }

        /// Timer pour deconnecter, le connecteur
        /// apres avoir allumer les
        /// Thruster, Tank, Batts, Ect
        /// DEFAULT VALUE: 1f
        void UpdateTimerDeco()
        {
            if (!isDecoConnector) { return; }

            timerDeconnection += 0.5f;
            if (timerDeconnection >= timerDeco)
            {
                managBlock.connectorShip.Disconnect();
                timerDeconnection = 0;
                isDecoConnector = false;
            }
        }
        #endregion

        #region Truster
        void DeactiveThrusters()
        {
            if (managBlock.listAllTrusters.Count != 0)
            {
                foreach (IMyThrust thrusterBlock in managBlock.listAllTrusters)
                {
                    if (thrusterBlock.Enabled)
                    {
                        thrusterBlock.Enabled = false;
                    }
                }
            }
        }
        void ActiveThrusters()
        {
            if (managBlock.listAllTrusters.Count != 0)
            {
                foreach (IMyThrust thrusterBlock in managBlock.listAllTrusters)
                {
                    if (!thrusterBlock.Enabled)
                    {
                        thrusterBlock.Enabled = true;
                    }
                }
            }
        }
        #endregion

        #region Tank
        void DestockTanks()
        {
            if (managBlock.listAllTanks.Count != 0)
            {
                foreach (IMyGasTank gasBlock in managBlock.listAllTanks)
                {
                    gasBlock.Stockpile = false;
                }
            }
        }
        void RestockTanks()
        {
            if (managBlock.listAllTanks.Count != 0)
            {
                foreach (IMyGasTank gasBlock in managBlock.listAllTanks)
                {
                    gasBlock.Stockpile = true;
                }
            }
        }
        #endregion

        #region Batt
        /// Ici on met toutes les batteries en recharge
        public void RechargeBattery()
        {
            if (managBlock.listAllBatterys.Count != 0)
            {
                foreach (IMyBatteryBlock batteryBlock in managBlock.listAllBatterys)
                {
                    batteryBlock.ChargeMode = ChargeMode.Recharge;
                }
            }
        }
        /// Ici on met toutes les batteries en auto
        public void AutoBattery()
        {
            if (managBlock.listAllBatterys.Count != 0)
            {
                foreach (IMyBatteryBlock batteryBlock in managBlock.listAllBatterys)
                {
                    batteryBlock.ChargeMode = ChargeMode.Auto;
                }
            }
        }
        #endregion
    }
}
