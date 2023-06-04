using System;
using System.Reflection;
using WWW.Domain.Entity;

namespace WWW.Service.Helpers
{
    public static class Marge<T>
    {
        public static void marge(T target, T source, string[] exeption = null)
        {
            Type userType = typeof(T);
            PropertyInfo[] properties = userType.GetProperties();

            var exept = new string[]{ "Id" };
            if (exeption != null ) exept = exept.Concat(exeption).ToArray();
            foreach (PropertyInfo property in properties)
            {
                // Проверяем, что свойство можно записать (есть set-метод)
                if (property.CanWrite)
                {
                    if (exept.Contains(property.Name)) continue;

                    //try
                    //{

                        object targetValue = property.GetValue(target);
                        object sourceValue = property.GetValue(source);

                        // Если значение из source не является null, присваиваем его в target
                        //if (sourceValue == targetValue) { continue; }
                        if (sourceValue != null)
                        {
                            property.SetValue(target, sourceValue);
                        }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine("!!! "+ex.Message);
                    //    continue;
                    //}

                }
            }
        }

    }
}
