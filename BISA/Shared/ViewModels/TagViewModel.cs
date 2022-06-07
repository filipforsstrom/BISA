using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.ViewModels
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string? Tag { get; set; }

        public override bool Equals(object o)
        {
            var other = o as TagViewModel;
            return other?.Id == Id;
        }
        public override int GetHashCode() => Id.GetHashCode();
        public override string ToString()
        {
            return Tag;
        }
    }
}
