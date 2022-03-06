using System.Collections.Generic;

namespace ReservationSystem.Shared.Contracts.Dtos
{
    public class DormitoriesDto : TableDto
    {
        public List<DormitoryDto> Rows { get; set; }
    }
}