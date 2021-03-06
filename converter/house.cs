//------------------------------------------------------------------------------
//fjklsdjflksakflsahflksahfksajdflkaskjflkjasdtklhjkbadlkjfhsadjkfkjhasflkjaskfhsalkdfhzkljblskd;fsd
//------------------------------------------------------------------------------

namespace converter
{
    using System;
    using System.Collections.Generic;
    
    public partial class house
    {
        public int PlaceId { get; set; }
        public Nullable<int> TotalFloors { get; set; }
        public Nullable<double> TotalArea { get; set; }
        public Nullable<int> TotalRooms { get; set; }
    
        public virtual place place { get; set; }
    }
}
