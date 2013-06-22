﻿namespace Timely.ViewModels.TaskList
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Timely.ViewModels.Base;

    public interface ITaskListViewModel : IViewModel
    {
        event EventHandler SelectedItemChanged;

        ICommand DeleteSelectedTaskCommand { get; }
        
        ICommand EditSelectedTaskCommand { get; }

        ObservableCollection<ITaskListItemViewModel> Items { get; }

        ITaskListItemViewModel SelectedItem { get; set; }

        ICommand StartSelectedTaskCommand { get; }

        ICommand StopSelectedTaskCommand { get; }
    }
}