using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Data
{
    public record BasicResponse
    {
        [Required]
        public string InvocationId { get; init; }
        [Required]
        public string Application { get; init; }
        [Required]
        public string Message { get; init; }
        public DateTimeOffset InvocationDate { get; init; }

    }
}
