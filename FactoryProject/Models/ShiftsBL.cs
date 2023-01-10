using System;
using FactoryProject.Data;

namespace FactoryProject.Models
{
	public class ShiftsBL
	{
        private readonly DataContext _context;

        public ShiftsBL(DataContext context)
        {
            _context = context;
        }

        public List<Shift> GetShifts() {
            return _context.Shift.ToList();
        }

        public Shift GetShift(int id) 
        {
            return _context.Shift.Where(shift => shift.id == id).First();
        }


        public void AddShift(Shift NewShift) 
        {
            _context.Shift.Add(NewShift);
            _context.Shift.SaveChanges();
        }
    }
}