﻿using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
    partial class Program
    {
        public class Debug
        {
            Program prgm;
            public string tempConstructionDebug;


            public Debug(Program _prgm)
            {
                this.prgm = _prgm;

                //ShowDebug(_prgm.managBlock);
            }

            public void ShowDebug(ManagerBlocks _managerBlocks)
            {
                tempConstructionDebug = string.Empty;
                tempConstructionDebug += "~~ DEBUG Auto-Dock ~~\n";
                if (_managerBlocks.listAllBatterys.Count != 0)
                {
                    tempConstructionDebug += "Nbs de batteries: " + _managerBlocks.listAllBatterys.Count;


                    int recharge = 0;
                    int auto = 0;
                    int decharge = 0;
                    foreach (IMyBatteryBlock batteryBlock in _managerBlocks.listAllBatterys)
                    {
                        if (batteryBlock.ChargeMode == ChargeMode.Recharge)
                        {
                            recharge++;
                        }
                        else if (batteryBlock.ChargeMode == ChargeMode.Auto)
                        {
                            auto++;
                        }
                        else
                        {
                            decharge++;
                        }
                    }

                    tempConstructionDebug += "\nRecharge On: " + recharge + " / Auto On: " + auto;
                    tempConstructionDebug += "\nDecharge On: " + decharge;
                }
                //tempConstructionDebug += "\n~~ DEBUG BLOCs Trusters ~~";

                //tempConstructionDebug += "\n~~ DEBUG BLOCs Connector ~~";
                if (_managerBlocks.listAllConnectors.Count != 0)
                {
                    tempConstructionDebug += "\nNbs de connectors: " + _managerBlocks.listAllConnectors.Count;

                    foreach (IMyShipConnector connectorBlock in _managerBlocks.listAllConnectors)
                    {
                        if (connectorBlock.BlockDefinition.SubtypeId == "ConnectorMedium")
                        {
                            //prgm.Echo("~~~~~____~~~~~");
                            tempConstructionDebug += "\nValide: " + connectorBlock.CustomName;
                            tempConstructionDebug += "\nEtat: " + connectorBlock.Status;

                        }


                    }

                    if (_managerBlocks.connectorShip == null)
                    {
                        tempConstructionDebug += "\nPas de connecteur valide";
                    }
                }
                //tempConstructionDebug += "\n~~ DEBUG BLOCs Tank ~~";
                if (_managerBlocks.listAllTanks.Count != 0)
                {
                    tempConstructionDebug += "\nNbs de tanks: " + _managerBlocks.listAllTanks.Count;

                    int on = 0;
                    int off = 0;
                    foreach (IMyGasTank tankBlock in _managerBlocks.listAllTanks)
                    {
                        if (tankBlock.Stockpile)
                        {
                            on++;
                        }
                        else
                        {
                            off++;
                        }
                    }
                    tempConstructionDebug += "\nStockpile On: " + on + " / Off: " + off;
                }


                if (_managerBlocks.listAllTrusters.Count != 0)
                {
                    tempConstructionDebug += "\nNbs de trusters: " + _managerBlocks.listAllTrusters.Count;
                    int on = 0;
                    int off = 0;
                    foreach (IMyThrust trusterBlock in _managerBlocks.listAllTrusters)
                    {
                        if (trusterBlock.Enabled)
                        {
                            on++;
                        }
                        else
                        {
                            off++;
                        }
                    }
                    tempConstructionDebug += "\nTruster On: " + on + " / Off: " + off;

                }

                tempConstructionDebug += "\n~~ by AANGEE & WALTO ~~";
                //Debug
                prgm.Echo(tempConstructionDebug);
            }

            string InfoEtatBattery(IMyBatteryBlock _batt)
            {
                string infoCharge = "";
                // On recup son etat de charge
                infoCharge = _batt.ChargeMode.ToString();

                return infoCharge;
            }
        }

    }
}