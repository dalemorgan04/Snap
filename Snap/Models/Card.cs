using Snap.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snap.Models
{
    public class Card
    {
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }
    }
}
