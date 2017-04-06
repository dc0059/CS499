using System;
namespace CS499.TCMS.Model
{

    /// <summary>
    /// this class will hold the Enums for the application
    /// </summary>
    public static class Enums
    {

        /// <summary>
        /// Truck capacity class
        /// </summary>
        public enum TruckMaxCapacity
        {
            class1 = 0,
            class2,
            class3,
            class4,
            class5,
            class6,
            class7,
            class8
        }

        /// <summary>
        /// User access level
        /// </summary>
        public enum AccessLevel
        {
            Full = 0,
            ShippingData,
            MaintenanceData,
            DriverData

        }

        /// <summary>
        /// Report types
        /// </summary>
        public enum ReportTypes
        {
            Payroll = 0,
            Maintenance_Cost,
            Vehicle_Maintenance,
            Incoming_Shipment,
            Outgoing_Shipment
        }

        /// <summary>
        /// Get the humanized names for the enum
        /// </summary>
        /// <param name="e">type of enum</param>
        /// <returns>string array of the humanized names</returns>
        public static string[] GetHumanizedValues<T>()
        {

            string[] values = Enum.GetNames(typeof(T));

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Replace('_', ' ');
            }

            Array.Sort(values);

            return values;

        }

    }
}