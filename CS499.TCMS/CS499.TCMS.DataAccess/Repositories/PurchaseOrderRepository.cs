using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.DataAccess;
using ToolKit.Data;
using CS499.TCMS.Model;
using System.Data;

namespace CS499.TCMS.DataAccess
{
    class PurchaseOrderRepository : GenericRepository<PurchaseOrder>, IPurchaseOrderRepository
    {

        #region Constructor
        public PurchaseOrderRepository(IDatabase database) : base(database)
        {
        }

        #endregion

        #region Methods
        public void Delete(PurchaseOrder model)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseorder " +
                              "WHERE OrderID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OrderID",
                Type = DbType.Int64,
                Value = model.OrderID
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void DeleteByDest(long DestComp)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseorder " +
                              "WHERE DestinationID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DestinationID",
                Type = DbType.Int64,
                Value = DestComp
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void DeleteByManifest(long ManifestID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseorder " +
                              "WHERE ManifestID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ManifestID",
                Type = DbType.Int64,
                Value = ManifestID
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void DeleteByNumber(long OrderNum)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseorder " +
                              "WHERE OrderNumber = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OderNumber",
                Type = DbType.Int64,
                Value = OrderNum
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void DeleteBySource(long SourceComp)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseorder " +
                              "WHERE SourceID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_SourceID",
                Type = DbType.Int64,
                Value = SourceComp
            });

            this.Database.ExecuteModQuery(definition);
        }

        public IEnumerable<PurchaseOrder> getAll()
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT OrderID, OrderNumber, SourceID, DestinationID, ManifestID " +
                              "FROM purchaseorder " +
                              "ORDER BY OrderID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            return this.Database.ExecuteListQuery<PurchaseOrder>(definition, Map);
        }

        public IEnumerable<PurchaseOrder> getOrderByDestination(long DestCompany)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT OrderID, OrderNumber, SourceID, DestinationID, ManifestID " +
                              "FROM purchaseorder " +
                              "WHERE DestinationID = ? " +
                              "ORDER BY OrderID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DestinationID",
                Type = DbType.Int64,
                Value = DestCompany
            });

            return this.Database.ExecuteListQuery<PurchaseOrder>(definition, Map);
        }

        public IEnumerable<PurchaseOrder> getOrderByManifest(long ManifestID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT OrderID, OrderNumber, SourceID, DestinationID, ManifestID " +
                              "FROM purchaseorder " +
                              "WHERE ManifestID = ? " +
                              "ORDER BY OrderID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ManifestID",
                Type = DbType.Int64,
                Value = ManifestID
            });

            return this.Database.ExecuteListQuery<PurchaseOrder>(definition, Map);
        }

        public IEnumerable<PurchaseOrder> getOrderByNumber(long OrderNum)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT OrderID, OrderNumber, SourceID, DestinationID, ManifestID " +
                              "FROM purchaseorder " +
                              "WHERE OrderNumber = ? " +
                              "ORDER BY OrderID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OrderNumber",
                Type = DbType.Int64,
                Value = OrderNum
            });

            return this.Database.ExecuteListQuery<PurchaseOrder>(definition, Map);
        }

        public IEnumerable<PurchaseOrder> getOrderBySource(long SourceCompany)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT OrderID, OrderNumber, SourceID, DestinationID, ManifestID " +
                              "FROM purchaseorder " +
                              "WHERE SourceID = ? " +
                              "ORDER BY OrderID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_SourceID",
                Type = DbType.Int64,
                Value = SourceCompany
            });

            return this.Database.ExecuteListQuery<PurchaseOrder>(definition, Map);
        }

        public PurchaseOrder getSingle(object id)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT OrderID, OrderNumber, SourceID, DestinationID, ManifestID " +
                              "FROM purchaseorder " +
                              "WHERE OrderID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OrderID",
                Type = DbType.Int64,
                Value = id
            });

            return this.Database.ExecuteSingleQuery<PurchaseOrder>(definition, Map);
        }

        /*public PurchaseOrder getSingle(long OrderID)
        {
            throw new NotImplementedException();
        }*/

        public void Insert(PurchaseOrder model)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "INSERT INTO purchaseorder (OrderNumber, SourceID, DestinationID, ManifestID) " +
                              "VALUES (?,?,?,?)",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OrderNumber",
                Type = DbType.Int64,
                Value = model.OrderNumber
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_SourceID",
                Type = DbType.Int64,
                Value = model.SourceID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DestinationID",
                Type = DbType.Int64,
                Value = model.DestinationID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ManifestID",
                Type = DbType.Int64,
                Value = model.ManifestID
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void Update(PurchaseOrder model)
        {
            //create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "UPDATE purchaseorder " +
                              "SET OrderNumber = ?, SourceID = ?, DestinationID = ?, ManifestID = ? " +
                              "WHERE OrderID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OrderNumber",
                Type = DbType.Int64,
                Value = model.OrderNumber
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_SourceID",
                Type = DbType.Int64,
                Value = model.SourceID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DestinationID",
                Type = DbType.Int64,
                Value = model.DestinationID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ManifestID",
                Type = DbType.Int64,
                Value = model.ManifestID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OrderID",
                Type = DbType.Int64,
                Value = model.OrderID
            });

            this.Database.ExecuteModQuery(definition);
        }

        protected override PurchaseOrder Map(IDataReader reader)
        {
            return new PurchaseOrder(reader.GetValueOrDefault<Int64>("OrderID"),
                reader.GetValueOrDefault<Int64>("OrderNumber"),
                reader.GetValueOrDefault<Int64>("SourceID"),
                reader.GetValueOrDefault<Int64>("DestinationID"),
                reader.GetValueOrDefault<Int64>("ManifestID"));
        }


        #endregion
    }
}
