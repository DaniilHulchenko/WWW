using System;
using System.Reflection;
using WWW.Domain.Entity;

namespace WWW.Service.Helpers
{
    public static class Merge<T>
    {
        public static void merge(T target, T source, string[] exeption = null,string[] nullIsDelete=null)
        {
            Type userType = typeof(T);
            PropertyInfo[] properties = userType.GetProperties();

            var exept = new string[]{ "Id" };
            if (exeption != null ) exept = exept.Concat(exeption).ToArray();

            if (nullIsDelete == null) nullIsDelete = new string[] {};
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
                    if (nullIsDelete.Contains(property.Name) && sourceValue == null) { property.SetValue(target, null); continue; }
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
