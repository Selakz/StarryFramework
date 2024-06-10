using UnityEngine;
using UnityEngine.Events;
using System;

namespace StarryFramework
{
    public class ResourceComponent : BaseComponent
    {

        private ResourceManager _manager = null;

        private ResourceManager manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = FrameworkManager.GetManager<ResourceManager>();
                }
                return _manager;
            }
        }
        private Type _targetType = null;

        private string _resourcePath = "";

        private LoadState _state = LoadState.Idle;

        private float _progress = 0;


        public LoadState State => _state;

        public float Progress => _progress;

        public string ResourcePath => _resourcePath;

        public Type TargetType => _targetType;

        private ResourceRequest latestRequest = null;

        protected override void Awake()
        {
            base.Awake();
            if (_manager == null)
            {
                _manager = FrameworkManager.GetManager<ResourceManager>();
            }
        }

        private void Update()
        {
            if(_state == LoadState.Loading)
            {
                _progress = latestRequest.progress;
            }
        }

        internal override void DisableProcess()
        {
            base.DisableProcess();
        }

        /// <summary>
        /// ͬ������һ����Դ
        /// </summary>
        /// <typeparam path="T">��Դ������</typeparam>
        /// <param path="path">��Դ��Resources�ļ���������·����</param>
        /// <param path="GameObjectInstantiate">�����Դ��GameObject�Ƿ�ֱ������</param>
        /// <returns>�����Ӧ��ԴΪGameobjet,�����ɲ��������壻������ǣ���ֱ�ӷ�������</returns>
        public T LoadRes<T>(string path, bool GameObjectInstantiate = false) where T : UnityEngine.Object
        {
            _targetType = typeof(T);
            _resourcePath = path;
            FrameworkManager.EventManager.InvokeEvent(FrameworkEvent.BeforeLoadAsset);
            T t =  manager.LoadRes<T>(path, GameObjectInstantiate);
            FrameworkManager.EventManager.InvokeEvent(FrameworkEvent.AfterLoadAsset);
            return t;
        }

        /// <summary>
        /// ͬ������·����������Դ
        /// </summary>
        /// <typeparam path="T">��Դ������</typeparam>
        /// <param path="path">·��</param>
        /// <returns>���ص���Դ����</returns>
        public T[] LoadAllRes<T>(string path) where T : UnityEngine.Object
        {
            _targetType = typeof(T);
            _resourcePath = path;
            FrameworkManager.EventManager.InvokeEvent(FrameworkEvent.BeforeLoadAsset);
            T[] t = manager.LoadAllRes<T>(path);
            FrameworkManager.EventManager.InvokeEvent(FrameworkEvent.AfterLoadAsset);
            return t;
        }

        /// <summary>
        /// �첽������Դ
        /// </summary>
        /// <typeparam path="T">��Դ����</typeparam>
        /// <param path="path">��Դ��Resources�ļ����µ�·����ʡ����չ��</param>
        /// <param path="callBack">��Դ������Ļص����Լ��ص���Դ����Ϊ����</param>
        /// <param path="GameObjectInstantiate">�����Դ��GameObject�Ƿ�ֱ������</param> 
        /// <returns>��Դ��������</returns>
        public ResourceRequest LoadAsync<T>(string path, UnityAction<T> callBack, bool GameObjectInstantiate = false) where T : UnityEngine.Object
        {
            _targetType = typeof(T);
            _resourcePath = path;
            FrameworkManager.EventManager.InvokeEvent(FrameworkEvent.BeforeLoadAsset);
            _state = LoadState.Loading;
            callBack += (a) => {
                _state = LoadState.Idle; 
                _progress = 1f; 
                FrameworkManager.EventManager.InvokeEvent(FrameworkEvent.AfterLoadAsset); 
            };
            ResourceRequest r = manager.LoadAsync<T>(path, callBack, GameObjectInstantiate);
            latestRequest = r;
            return r;
        }

        /// <summary>
        /// ֻ��ж�ط�GameObject����, GameObject������Destroy����
        /// </summary>
        /// <param path="_object"></param>
        public void Unload(UnityEngine.Object _object)
        {
            manager.Unload(_object);
        }

        /// <summary>
        /// �ͷ�����û��ʹ�õ���Դ
        /// </summary>
        public void UnloadUnused()
        {
            manager.UnloadUnused();
        }



    }
}

