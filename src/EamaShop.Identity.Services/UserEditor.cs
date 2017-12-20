using EamaShop.Infrastructures.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EamaShop.Identity.Services
{
    public abstract class UserEditor
    {

        /// <summary>
        /// Set user's nickname
        /// </summary>
        /// <exception cref="ArgumentNullException">value can not be null.</exception>
        public abstract string NickName { set; }
        public abstract string HeadImageUri { set; }
        public abstract Gender Sexy { set; }

        public abstract string Country { set; }

        public abstract string City { set; }

        public abstract string Province { set; }
    }
}
