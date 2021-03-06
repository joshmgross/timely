﻿namespace Timely.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Timely.Models.Common;
    using Timely.Models.Entities;
    using Timely.Models.Serialization;

    public class TasksModel : EntityModel<Task>, ITasksModel
    {
        public TasksModel(ITaskListStore taskListStore, IEntityCreator<Task> entityCreator)
            : base(taskListStore.Tasks, entityCreator)
        {
        }

        public Task Add(string description)
        {
            return Add(description, Guid.Empty);
        }

        public Task Add(string description, Guid groupId)
        {
            Task task = CreateEntity(description, groupId);
            AddToStore(task);
            return task;
        }

        public void SetTaskIndex(Guid id, int index)
        {
            Task task = EntityDictionary[id];
            if (task.Index != index)
            {
                List<Task> tasks = EntityDictionary.Values.OrderBy(t => t.Index).ToList();

                index = GetSafeIndex(index, tasks);

                tasks.Remove(task);
                tasks.Insert(index, task);

                ReIndexTasks(tasks);
            }
        }

        static int GetSafeIndex(int index, List<Task> tasks)
        {
            return Math.Max(0, Math.Min(tasks.Count, index));
        }

        static void ReIndexTasks(IEnumerable<Task> tasks)
        {
            int i = 0;
            foreach (Task t in tasks)
                t.Index = i++;
        }

        Task CreateEntity(string description, Guid groupId)
        {
            Task task = EntityCreator.Create();
            task.Description = description;
            task.GroupId = groupId;
            task.Index = GetNewTaskIndex();
            return task;
        }

        int GetNewTaskIndex()
        {
            if (EntityDictionary.Count > 0)
                return EntityDictionary.Values.Max(t => t.Index) + 1;
            return 0;
        }
    }
}