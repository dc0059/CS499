using System;
namespace CS499.TCMS.DataAccess
{

    /// <summary>
    /// this class will hold the enums for the application
    /// </summary>
    public static class Enums
    {

        /// <summary>
        /// This will be the enum for the notification tpyes
        /// </summary>
        public enum JobType
        {
            Ticket = 0,
            OutOfStock,
            FruAuthorization,
            MmaAuthorization
        }

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