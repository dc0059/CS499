using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolKit.Data;
using CS499.TCMS.Model;
using System.Data;
using CS499.TCMS.DataAccess.IRepositories;

namespace CS499.TCMS.DataAccess.Repositories
{
    class PurchaseItemRepository : GenericRepository<PurchaseItem>, IPurchaseItemRepository
    {
        #region Constructor

        public PurchaseItemRepository(IDatabase database) : base(database)
        {
        }

        #endregion

        #region Methods

        public void Delete(PurchaseItem model)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseitems " +
                              "WHERE ItemID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ItemID",
                Type = DbType.Int64,
                Value = model.ItemID
            });

            this.Database.ExecuteModQuery(definition);

            // Create query definition
            definition = new QueryDefinition()
            {
                CommandText = "UPDATE purchaseitems_log " +
                              "SET DeletedBy = ? " +
                              "WHERE ItemID = ? " +
                              "AND ModifiedStatus = 'D'",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition            
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_User",
                Type = DbType.String,
                Value = this.Database.UserName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ID",
                Type = DbType.Int64,
                Value = model.ItemID
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void Delete(long ItemID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseitems " +
                              "WHERE ItemID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ItemID",
                Type = DbType.Int64,
                Value = ItemID
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void DeleteItemsByOrderID(long OrderID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseitems " +
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
                Value = OrderID
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void DeleteItemsByPartID(long PartID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseitems " +
                              "WHERE PartID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartID",
                Type = DbType.Int64,
                Value = PartID
            });

            this.Database.ExecuteModQuery(definition);
        }

        public IEnumerable<PurchaseItem> GetAll()
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT ItemID, OrderID, Quantity, PartID " +
                              "FROM purchasitems " +
                              "ORDER BY ItemID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            return this.Database.ExecuteListQuery<PurchaseItem>(definition, Map);
        }

        public IEnumerable<PurchaseItem> GetItemsByOrderID(long OrderID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT ItemID, OrderID, Quantity, PartID " +
                              "FROM purchasitems " +
                              "WHERE OrderID = ? " +
                              "ORDER BY ItemID",
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
                Value = OrderID
            });

            return this.Database.ExecuteListQuery<PurchaseItem>(definition, Map);
        }

        public IEnumerable<PurchaseItem> GetItemsByPart(long PartID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT ItemID, OrderID, Quantity, PartID " +
                              "FROM purchasitems " +
                              "WHERE PartID = ? " +
                              "ORDER BY ItemID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartID",
                Type = DbType.Int64,
                Value = PartID
            });

            return this.Database.ExecuteListQuery<PurchaseItem>(definition, Map);
        }

        public PurchaseItem GetSingle(object id)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT ItemID, OrderID, Quantity, PartID " +
                              "FROM purchaseitems " +
                              "WHERE ItemID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ItemID",
                Type = DbType.Int64,
                Value = id
            });

            return this.Database.ExecuteSingleQuery<PurchaseItem>(definition, Map);
        }

        /*public PurchaseItem GetSingle(long ItemID)
        {
            throw new NotImplementedException();
        }*/

        public void Insert(PurchaseItem model)
        {

            long id;

            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "INSERT INTO purchaseitems (OrderID, Quantity, PartID, CreatedBy, LastModifiedBy) " +
                              "VALUES (?,?,?,?,?)",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OrderID",
                Type = DbType.Int64,
                Value = model.OrderID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Quantity",
                Type = DbType.Int16,
                Value = model.Quantity
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartID",
                Type = DbType.Int64,
                Value = model.PartID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_CreatedBy",
                Type = DbType.String,
                Value = this.Database.UserName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_LastModifiedBy",
                Type = DbType.String,
                Value = this.Database.UserName
            });

            this.Database.ExecuteModQuery(definition, out id);

            model.ItemID = id;
        }

        public void Update(PurchaseItem model)
        {
            //create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "UPDATE purchaseitems " +
                              "SET OrderID, Quantity, PartID, LastModifiedBy = ? " +
                              "WHERE ItemID = ?",
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
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Quantity",
                Type = DbType.Int16,
                Value = model.Quantity
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PartID",
                Type = DbType.Int64,
                Value = model.PartID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_LastModifiedBy",
                Type = DbType.String,
                Value = this.Database.UserName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ItemID",
                Type = DbType.Int64,
                Value = model.ItemID
            });

            this.Database.ExecuteModQuery(definition);
        }

        protected override PurchaseItem Map(IDataReader reader)
        {
            return new PurchaseItem(reader.GetValueOrDefault<long>("ItemID"),
                reader.GetValueOrDefault<long>("OrderID"),
                reader.GetValueOrDefault<int>("Quantity"),
                reader.GetValueOrDefault<long>("PartID"));
        }

        #endregion
    }
}
