using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace GAImageApproxUX
{
    class ThreadSafeServiceUX
    {
        // delegate method
        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        /// <summary>
        /// Thread safe: sets the value of the win. form object.
        /// </summary>
        /// <param name="control">Win. form object.</param>
        /// <param name="propertyName">Object property.</param>
        /// <param name="propertyValue">New value.</param>
        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate
                (SetControlPropertyThreadSafe),
                new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(
                    propertyName,
                    BindingFlags.SetProperty,
                    null,
                    control,
                    new object[] { propertyValue });
            }
        }

        /// <summary>
        /// Selects all non-abstract classes implementing the interface TInterface.
        /// </summary>
        /// <typeparam name="TInterface">Interfface.</typeparam>
        /// <returns>Non-abstract classe.</returns>
        public static Dictionary<string, Type> selectType<TInterface>()
        {
            
            var result = new Dictionary<string, Type>();

            var type = typeof(TInterface);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) & p.IsClass & !p.IsAbstract).ToArray();



            foreach (var t in types)
            {
                result.Add(t.Name, t);
            }

            return result;
        }
    }
}
