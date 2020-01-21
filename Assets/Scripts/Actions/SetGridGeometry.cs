using Grid;
using UnityEngine;
using Variables;

namespace Actions
{
    public class SetGridGeometry : MonoBehaviour
    {
        public GridGeometryVariable variable;
        public GridGeometry value;
    
        public void Apply()
        {
            variable.Value = value;
        }
    }
}
