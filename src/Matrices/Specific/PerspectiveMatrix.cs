using System;

namespace Zene.Structs
{
    public class PerspectiveMatrix : IMatrix, ISizeable
    {
        public PerspectiveMatrix() { }
        public PerspectiveMatrix(Radian fovy, floatv aspect, floatv depthNear, floatv depthFar)
        {
            _aspect = aspect;
            Fovy = fovy;
            _depthN = depthNear;
            DepthFar = depthFar;
        }

        public int Rows => 4;
        public int Columns => 4;
        
        public bool Constant => false;
        
        private Radian _fov;
        public Radian Fovy
        {
            get => _fov;
            set
            {
                if (value <= 0 || value > Math.PI)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _fov = value;
                floatv d = 1 / Maths.Tan(value * 0.5f);
                _v1 = d / _aspect;
                _v2 = d;
            }
        }
        private floatv _aspect;
        public floatv Aspect
        {
            get => _aspect;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _aspect = value;
                _v1 = _v2 / value;
            }
        }
        private floatv _depthN;
        public floatv DepthNear
        {
            get => _depthN;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _depthN = value;
                floatv dm = 1 / (_depthF - value);
                floatv v3 = _depthF * dm;
                _v3 = v3;
                _v4 = -value * v3;
            }
        }
        private floatv _depthF;
        public floatv DepthFar
        {
            get => _depthF;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _depthF = value;
                floatv dm = 1 / (value - _depthN);
                floatv v3 = value * dm;
                _v3 = v3;
                _v4 = -_depthN * v3;
            }
        }
        
        private floatv _v1;
        private floatv _v2;
        private floatv _v3;
        private floatv _v4;

        Vector2I ISizeable.Size { set => Aspect = (floatv)value.X / (floatv)value.Y;  }

        public void MatrixData(MatrixSpan ms)
        {
            // 4x4 only
            if (ms.Rows != 4 || ms.Columns != 4)
            {
                ms.Padding(0, 0);
                return;
            }
            
            ms.Data[0] = _v1;
            ms.Data[5] = _v2;
            ms.Data[10] = _v3;
            ms.Data[11] = 1;
            ms.Data[14] = _v4;
        }
    }
}
