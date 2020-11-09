using FindIt.API.Models.Accounts;
using FindIt.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.API.Helpers
{
    public static class DomainExtension
    {
    
        public static IEnumerable<AccountsVM> ConvertAllToViewModels(this IEnumerable<Account> userDomains)
            {
                foreach (Account user in userDomains)
                {
                    yield return user.ConvertToViewModel();
                }
            }

            public static AccountsVM ConvertToViewModel(this Account userDomains)
            {
            AccountsVM userViewModel = new AccountsVM()
                {

                    Id = userDomains.Id
                    ,
                    FirstName = userDomains.FirstName
                    ,
                    LastName = userDomains.LastName
                    ,
                    Email = userDomains.Email
                    ,
                    Created = userDomains.Created
                    ,
                    Role = userDomains.Role
                      
            };

                return userViewModel;
            }
        }
    }

