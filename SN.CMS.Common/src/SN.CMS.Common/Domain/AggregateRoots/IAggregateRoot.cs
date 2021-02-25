using System;

namespace SN.CMS.Common.Domain.AggregateRoots
{
    public interface IAggregateRoot : IAggregateRoot<Guid>
    {
    }

    public interface IAggregateRoot<TKey>
    {
        TKey Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime? ModifyTime { get; set; }


        bool IsDeleted { get; set; }

        DateTime? DeleteTime { get; set; }
    }
}
