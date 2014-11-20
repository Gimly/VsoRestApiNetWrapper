using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VsoRestApiNetWrapper
{
    /// <summary>
    /// Defines the state which the team projects can be in.
    /// </summary>
    public enum ProjectState
    {
        /// <summary>
        /// Team project is completely created and ready to use.
        /// </summary>
        WellFormed,
        /// <summary>
        /// Team project has been queued for creation, but the process has not yet started.
        /// </summary>
        CreatePending,
        /// <summary>
        /// Team project is in the process of being deleted.
        /// </summary>
        Deleting,
        /// <summary>
        /// Team project is in the process of being created.
        /// </summary>
        New
    }

    /// <summary>
    /// Defines a filter that can be used to retrieve team projects
    /// based on their state.
    /// </summary>
    public enum ProjectStateFilter
    {
        /// <summary>
        /// Team project is completely created and ready to use.
        /// </summary>
        WellFormed,
        /// <summary>
        /// Team project has been queued for creation, but the process has not yet started.
        /// </summary>
        CreatePending,
        /// <summary>
        /// Team project is in the process of being deleted.
        /// </summary>
        Deleting,
        /// <summary>
        /// Team project is in the process of being created.
        /// </summary>
        New,
        /// <summary>
        /// All team projects regardless of state.
        /// </summary>
        All
    }
}
