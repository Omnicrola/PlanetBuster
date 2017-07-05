using System.Collections.Generic;
using Assets.Scripts.Models;

namespace Assets.Scripts.Core.Levels
{
    public interface ILevelManager
    {
        List<ILevelDataController> GetAll();
    }
}