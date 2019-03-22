using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Entities
{

    public class BaseUserEntity
    {
        public static readonly List<BaseUserEntity> Users = new List<BaseUserEntity>();

        static BaseUserEntity()
        {
            Users.Add(new BaseUserEntity() {
                Id=0,
                UserName="admin",
                Password="11111"
            });
            Users.Add(new BaseUserEntity()
            {
                Id = 1,
                UserName = "red",
                Password = "11111"
            });
            Users.Add(new BaseUserEntity()
            {
                Id = 2,
                UserName = "yellow",
                Password = "11111"
            });
            Users.Add(new BaseUserEntity()
            {
                Id = 3,
                UserName = "blue",
                Password = "11111"
            });
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string salt { get; set; }
    }
}
