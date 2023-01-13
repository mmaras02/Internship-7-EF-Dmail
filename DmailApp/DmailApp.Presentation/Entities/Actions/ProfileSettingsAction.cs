//using DmailApp.Domain.Factories;
//using DmailApp.Domain.Repositories;
//using DmailApp.Presentation.Entities.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DmailApp.Presentation.Entities.Actions;

//public class ProfileSettingsAction : IAction
//{
//    public int UserId;
//    public IAction Action()
//    {
//        //e mail adrese ljudi koji su isključivo njemu slali poštu ili događaje
//        ///zajedno sa adresama kojima je isti taj logirani korisnik slao mailove ili događaje.
//        //odvoji spam--oznaci kao spam
//        //           --ukloni spam

//        var userRepository = RepositoryFactory.Create<UserRepository>();
//        var user = userRepository.GetById(UserId);

//    }
//}
