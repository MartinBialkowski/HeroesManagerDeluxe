using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using HeroesDataAccessLayer;
using GalaSoft.MvvmLight.Messaging;

namespace HeroesManagerDeluxe.ViewModel
{
    //Very Important EF must be installed in every project (future use)
    public class MainViewModel : WorkspaceViewModel
    {
        private ObservableCollection<WorkspaceViewModel> workspaces;
        readonly HeroesDAO heroesDAO;

        public MainViewModel()
        {
            heroesDAO = new HeroesDAO();
            HeroesListViewModel workspace = new HeroesListViewModel(heroesDAO);
            Workspaces.Add(workspace);

            RegisterMessage();
        }

        /// <summary>
        /// Getter for list of workspaces
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (workspaces == null)
                {
                    workspaces = new ObservableCollection<WorkspaceViewModel>();
                    workspaces.CollectionChanged += OnWorkspaceChanged;
                    //FINALLY dont forget to dispose of it -= ONWorkspaceChanged
                }
                return workspaces;
            }
        }

        private void RegisterMessage()
        {
            Messenger.Default.Register<DisplayWorkspaceMessage>(this, (msg) =>
            {
                //TODO ask about it. Adding same workspace
                //1. Easy to do  let only 1 HeroDetail at once
                //2. Hard search through list find every HeroDetail, then compare
                //theirs heroName with msg.workspace.heroName
                //right now user can adds multiply tabs of Abathur or other heroes
                Workspaces.Add(msg.workspace);
                SetActiveWorkspace(msg.workspace);
            });
        }

        /// <summary>
        /// Sets workspace sent as argument as active tab item
        /// </summary>
        /// <param name="workspace">ViewModel implementing WorkspaceViewModel</param>
        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(Workspaces);
            if (collectionView != null)
            {
                collectionView.MoveCurrentTo(workspace);
            }
        }

        void OnWorkspaceChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
            {
                foreach (WorkspaceViewModel workspace in e.NewItems)
                {
                    workspace.RequestClose += OnWorkspaceRequestClose;
                }
            }
            if (e.OldItems != null && e.OldItems.Count != 0)
            {
                foreach (WorkspaceViewModel workspace in e.OldItems)
                {
                    workspace.RequestClose -= OnWorkspaceRequestClose;
                }
            }
        }

        void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            workspace.Dispose();
            Workspaces.Remove(workspace);
        }
    }
}
