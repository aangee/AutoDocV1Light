using Sandbox.ModAPI.Ingame;
using System.Collections.Generic;
using System.Linq;

namespace IngameScript
{
    partial class Program
    {
        public class ManagerBlocks
        {
            Program prgm;

            // Liste tempon qui recup tous les blocs du ship et tous se qui se trouve connecter a la grid ou se trouve le programBloc
            List<IMyCockpit> plistAllCockpit = new List<IMyCockpit>();
            List<IMyBatteryBlock> plistAllBatterys = new List<IMyBatteryBlock>();
            List<IMyThrust> plistAllTrusters = new List<IMyThrust>();
            List<IMyShipConnector> plistAllConnectors = new List<IMyShipConnector>();
            List<IMyGasTank> plistAllTanks = new List<IMyGasTank>();

            // Liste des bloc du ship apres le tri
            public List<IMyCockpit> listAllCockpit = new List<IMyCockpit>();
            public List<IMyBatteryBlock> listAllBatterys = new List<IMyBatteryBlock>();
            public List<IMyThrust> listAllTrusters = new List<IMyThrust>();
            public List<IMyShipConnector> listAllConnectors = new List<IMyShipConnector>();
            public List<IMyGasTank> listAllTanks = new List<IMyGasTank>();

            //Connector et cockpit du ship
            public IMyShipConnector connectorShip;
            public IMyCockpit cockpitShip;

            public ManagerBlocks(Program _prgm)
            {
                this.prgm = _prgm;
                GetAllBlocks();//On recup tous les bloc
                BlockSorting();//On tri les bloc util
                FindCockpit();//On cherche un cockpit
                FindConnector();//On cherche le connector
            }

            #region Private
            /// <summary>
            /// Remplis nos liste tampon,
            /// pour faire un tri par la suite
            /// </summary>
            void GetAllBlocks()
            {
                prgm.GridTerminalSystem.GetBlocksOfType<IMyCockpit>(plistAllCockpit);
                prgm.GridTerminalSystem.GetBlocksOfType<IMyBatteryBlock>(plistAllBatterys);
                prgm.GridTerminalSystem.GetBlocksOfType<IMyThrust>(plistAllTrusters);
                prgm.GridTerminalSystem.GetBlocksOfType<IMyShipConnector>(plistAllConnectors);
                prgm.GridTerminalSystem.GetBlocksOfType<IMyGasTank>(plistAllTanks);
            }

            /// <summary>
            /// Tri de notre liste de bloc
            /// on recup l'uniqueID de le grid via le progamBloc
            /// et on check si les blocs de nos liste tempon on cette uniqueID
            /// ensuie on les ajoute a nos liste perso ( les bloc utils et qui sur la meme grid ) 
            /// </summary>
            void BlockSorting()
            {
                plistAllCockpit.ForEach(x =>
                {
                    if (x.CubeGrid.Name.Contains(prgm.uidGridShip))
                    {
                        listAllCockpit.Add(x);
                    }
                });
                plistAllBatterys.ForEach(x =>
                {
                    if (x.CubeGrid.Name.Contains(prgm.uidGridShip))
                    {
                        listAllBatterys.Add(x);
                    }
                });

                plistAllTrusters.ForEach(x =>
                {
                    if (x.CubeGrid.Name.Contains(prgm.uidGridShip))
                    {
                        listAllTrusters.Add(x);
                    }
                });
                plistAllConnectors.ForEach(x =>
                {
                    if (x.CubeGrid.Name.Contains(prgm.uidGridShip))
                    {
                        listAllConnectors.Add(x);
                    }
                });
                plistAllTanks.ForEach(x =>
                {
                    if (x.CubeGrid.Name.Contains(prgm.uidGridShip))
                    {
                        listAllTanks.Add(x);
                    }
                });
            }

            /// <summary>
            /// On recherche le cockpit.
            /// @info Pour le moment on prent le premier dans la liste
            /// </summary>
            void FindCockpit()
            {
                if (listAllCockpit.Count == 0)
                {
                    prgm.Echo("Cockpit non valide");
                    return;
                }
                //TODO: recherche du cockpit, avoir si on recuperai pas, par un tag (pour eviter les sousi de plusieur cockpit sur le ship)
                cockpitShip = listAllCockpit.First();

                prgm.Echo("Cockpit valide");
                if(prgm.isActiveFullDebug) prgm.Echo("CustomData: " + cockpitShip.CustomData);
            }
            /// <summary>
            /// On cherche le connecteur.
            /// Ici on cherche juste un connector petit bloc
            /// </summary>
            void FindConnector()
            {
                if (listAllConnectors.Count == 0) return;

                foreach (IMyShipConnector connector in listAllConnectors)
                {
                    // FIXME: pas sur: On doit recup toute la definition du bloc
                    // juste pour etre sur que le jeu nous donne se que l'on veux, 
                    VRage.ObjectBuilders.SerializableDefinitionId defId = connector.BlockDefinition;
                    // Verification que sais bien un connecteur et pas autre chose style les ( ejecter )
                    if (defId.SubtypeId.Contains("ConnectorMedium"))
                    {
                        connectorShip = connector;

                        prgm.Echo("Connecteur valide");
                        if (prgm.isActiveFullDebug) prgm.Echo("CustomData: " + connectorShip.CustomData);
                    }
                    else
                    {
                        if (prgm.isActiveDebugInProgramBloc)
                            prgm.Echo(connector.DisplayNameText + " n'ai pas un connecteur valide");
                    }
                }
            }
            #endregion
        }
    }
}
