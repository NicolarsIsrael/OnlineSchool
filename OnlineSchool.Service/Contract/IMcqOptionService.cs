using OnlineSchool.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Service.Contract
{
    public interface IMcqOptionService
    {
        Task Update(McqOption mcqOption);
        McqOption Get(int id);
    }
}
