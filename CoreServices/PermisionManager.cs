using DatabaseDomain.Entities;
using DatabaseDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreServices
{
    public class PermisionManager
    {
        public static class Permision
        {
            #region Security
            public const string Security_Roles_HttpGet = "54BA3190-F894-495B-A992-8ED280C3AD75";
            public const string Security_CreateRole_HttpGet = "3C20AAFB-8B7F-4FFF-B55F-FA32F073B24E";
            public const string Security_CreateRole_HttpPost = "5EE641AE-D2AB-4CAC-8F8F-C51C79EC435A";
            public const string Security_EditRole_HttpGet = "F0152D9A-9BE9-47CF-8923-DA2DE2A95AF7";
            public const string Security_EditRole_HttpPost = "188550AA-B841-4D5C-90A9-FC56BA9003BD";
            public const string Security_DeleteRole_HttpGet = "62524A50-1B47-43E7-B435-0290D1FFFBD5";
            #endregion
        }

        public static List<KeyValuePair<string, string>> GetPermisions()
        {
            var type = typeof(Permision);
            var fields = type.GetFields();

            var permisionsList = new List<KeyValuePair<string, string>>();

            foreach (var item in fields)
            {
                var value = item.GetValue(type);
                var title = item.Name.Replace("_", " ");

                permisionsList.Add(new KeyValuePair<string, string>(title, value.ToString()));
            }

            return permisionsList;
        }

        public static async Task SetPermisions(IUnitOfWork context)
        {
            var databasePermisions = await context._permision.GetAll();
            var classPermisions = GetPermisions();

            var newPermisions = new List<PermisionDomain>();

            foreach (var item in classPermisions)
            {
                if (!databasePermisions.Any(r => r.Value == item.Value))
                {
                    newPermisions.Add(new PermisionDomain()
                    {
                        Value = item.Value,
                        Title = item.Key
                    });
                }
            }

            if (await context._permision.AddRange(newPermisions))
            {
                context.Commit();
            }


        }

    }
}
