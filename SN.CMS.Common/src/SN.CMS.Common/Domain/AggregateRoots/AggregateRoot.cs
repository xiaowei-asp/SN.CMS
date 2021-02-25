using System;

namespace SN.CMS.Common.Domain.AggregateRoots
{
    public abstract class AggregateRoot : AggregateRoot<Guid>, IAggregateRoot
    {
        protected AggregateRoot()
        {
            Id = Guid.NewGuid();
        }

        protected AggregateRoot(Guid id)
        {
            Id = id;
        }

        protected virtual void SetUpdatedDate()
            => ModifyTime = DateTime.Now;

        protected virtual void SetDeleteDate()
            => DeleteTime = DateTime.Now;
    }

    public abstract class AggregateRoot<TKey> : IAggregateRoot<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>

        public virtual DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? ModifyTime { get; set; }
    }
}
