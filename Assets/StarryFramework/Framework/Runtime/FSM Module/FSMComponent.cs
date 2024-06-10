using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace StarryFramework
{
    public class FSMComponent : BaseComponent
    {
        private FSMManager _manager = null;

        private FSMManager manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = FrameworkManager.GetManager<FSMManager>();
                }
                return _manager;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            if (_manager == null)
            {
                _manager = FrameworkManager.GetManager<FSMManager>();
            }
        }

        internal override void Shutdown()
        {
            
        }



        /// <summary>
        /// ��ȡ����״̬������
        /// </summary>
        /// <returns></returns>
        public int GetFSMCount() => manager.FSMCount;

        /// <summary>
        /// ��������״̬����������ӵ�������ͺ�������ȫ��ͬ��״̬��
        /// </summary>
        /// <typeparam Name="T">ӵ��������</typeparam>
        /// <param Name="name">״̬������</param>
        /// <param Name="owner">ӵ����</param>
        /// <param Name="states">״̬�б�</param>
        /// <returns>״̬��</returns>
        public IFSM<T> CreateFSM<T>(string name, T owner, List<FSMState<T>> states) where T : class
        {
            return manager.CreateFSM<T>(name, owner, states);
        }

        /// <summary>
        /// ��������״̬����������ӵ�������ͺ�������ȫ��ͬ��״̬��
        /// </summary>
        /// <typeparam Name="T">ӵ��������</typeparam>
        /// <param Name="name">״̬������</param>
        /// <param Name="owner">ӵ����</param>
        /// <param Name="states">״̬����</param>
        /// <returns>״̬��</returns>
        public IFSM<T> CreateFSM<T>(string name, T owner, FSMState<T>[] states) where T : class
        {
            return manager.CreateFSM(name, owner, states);
        }

        /// <summary>
        /// ע������״̬��
        /// </summary>
        /// <typeparam Name="T">ӵ��������</typeparam>
        /// <param Name="name">״̬������</param>
        public void DestroyFSM<T>(string name) where T : class
        {
            manager.DestroyFSM<T>(name);
        }

        /// <summary>
        /// ע������״̬��
        /// </summary>
        /// <typeparam Name="T">ӵ��������</typeparam>
        /// <param Name="_fsm">״̬��</param>
        public void DestroyFSM<T>(IFSM<T> _fsm) where T : class
        {
            manager.DestroyFSM<T>(_fsm);
        }

        /// <summary>
        /// ��ѯ�Ƿ�ӵ��ĳ״̬��
        /// </summary>
        /// <typeparam Name="T">ӵ��������</typeparam>
        /// <param Name="name">״̬������</param>
        /// <returns></returns>
        public bool HasFSM<T>(string name) where T : class
        {
            return manager.HasFSM<T>(name);
        }

        /// <summary>
        /// ���ĳ״̬��
        /// </summary>
        /// <typeparam Name="T">ӵ��������</typeparam>
        /// <param Name="name">״̬������</param>
        /// <returns></returns>
        public IFSM<T> GetFSM<T>(string name) where T : class
        {
            return manager.GetFSM<T>(name);
        }

        /// <summary>
        /// ��ȡ����״̬��
        /// </summary>
        /// <returns></returns>
        public FSMBase[] GetAllFSMs()
        {
            return manager.GetAllFSMs();
        }


    }
}

