using System.Collections.Generic;
using commandAPI.Data;
using CommandAPI.Models;
using System.Linq;
using System;

namespace commandAPI.Data
{
    public class SqlCommandRepo : ICommandRepo
    {
        private readonly CommadContext _context;
        public SqlCommandRepo(CommadContext context)
        {
            _context = context;
        }
        public IEnumerable<Command> GetAllCommands() => _context.Commands.ToList();

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }

        void ICommandRepo.CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Commands.Add(cmd);
        }

        void ICommandRepo.DeleteCommand(Command cmd)
        {

            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Commands.Remove(cmd);
        }

        bool ICommandRepo.SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        void ICommandRepo.UpdateCommand(Command cmd)
        {
            //nothing
        }
    }
}