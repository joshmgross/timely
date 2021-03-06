﻿namespace Timely.Models.Models
{
    using System;
    using Timely.Models.Common;
    using Timely.Models.Entities;

    public interface IGroupsModel : IEntityModel<Group>
    {
        Group Add(string name);

        string GetGroupName(Guid groupId);
    }
}