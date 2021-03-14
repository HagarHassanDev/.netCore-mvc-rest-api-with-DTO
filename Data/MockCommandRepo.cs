using System.Collections.Generic;
using commandAPI.Data;
using CommandAPI.Models;

namespace CommandAPI.Data
{

    public class MockCommandRepo : ICommandRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                 new Command{Id=0 , HowTo="Hello hoeto ", Line= "This is line" , Platform ="Platform1"}
               , new Command{Id=1 , HowTo="Hello hoeto2 ", Line= "This is line2", Platform="platform 2"}
               , new Command{Id=2 , HowTo="Hello hoeto 3", Line= "This is line3",Platform="platform3" }
               , new Command{Id=3 , HowTo="Hello hoeto4 ", Line= "This is line4", Platform ="platform 4"}
            };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Hello hoeto ", Line = "This is line" };
        }

        void ICommandRepo.CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        void ICommandRepo.DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        bool ICommandRepo.SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        void ICommandRepo.UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }

}