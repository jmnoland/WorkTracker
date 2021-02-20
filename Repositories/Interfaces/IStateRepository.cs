﻿using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Models.DataModels;

namespace WorkTracker.Repositories.Interfaces
{
    public interface IStateRepository
    {
        void CreateDefaultStates(int teamId);
        List<Models.ServiceModels.State> GetByTeamId(int teamId);
    }
}
