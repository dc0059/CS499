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
            class_1 = 0,
            class_2,
            class_3,
            class_4,
            class_5,
            class_6,
            class_7,
            class_8
        }

        /// <summary>
        /// User access level
        /// </summary>
        public enum AccessLevel
        {
            Full = 0,
            Shipping_Data,
            Maintenance_Data,
            Driver_Data

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