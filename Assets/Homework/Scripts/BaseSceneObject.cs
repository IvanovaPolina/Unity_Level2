using UnityEngine;

namespace Homework
{
	/// <summary>
	/// Базовый класс всех объектов на сцене
	/// </summary>
	public abstract class BaseSceneObject : MonoBehaviour
	{
		protected GameObject _instanceObject;
		protected Transform _transform;
		protected Vector3 _position;
		protected Quaternion _rotation;
		protected Vector3 _scale;
		protected string _name;
		protected int _layer;
		protected Material _material;
		protected Color _color;
		protected Rigidbody _rigidbody;
		protected Collider _collider;
		protected bool _isVisible;
		protected AudioSource _audioSource;
		protected ParticleSystem _particleSystem;
		protected Animator _animator;

		protected virtual void Awake() {
			_instanceObject = gameObject;
			_transform = _instanceObject.transform;
			_name = _instanceObject.name;
			if (GetComponent<Renderer>()) {
				_material = GetComponent<Renderer>().material;
			}
			_rigidbody = _instanceObject.GetComponent<Rigidbody>();
			_collider = _instanceObject.GetComponent<Collider>();
			_audioSource = _instanceObject.GetComponent<AudioSource>();
			_particleSystem = _instanceObject.GetComponent<ParticleSystem>();
			_animator = _instanceObject.GetComponent<Animator>();
		}

		#region Properties
		/// <summary>
		/// Ссылка на gameObject
		/// </summary>
		public GameObject InstanceObject { get { return _instanceObject; } }
		/// <summary>
		/// Получить Transform объекта
		/// </summary>
		public Transform Transform { get { return _transform; } }
		/// <summary>
		/// Позиция объекта
		/// </summary>
		public Vector3 Position
		{
			get {
				if (InstanceObject != null)
					_position = Transform.position;
				return _position;
			}
			set {
				_position = value;
				if (InstanceObject != null)
					Transform.position = _position;
			}
		}
		/// <summary>
		/// Поворот объекта
		/// </summary>
		public Quaternion Rotation
		{
			get {
				if (InstanceObject != null)
					_rotation = Transform.rotation;
				return _rotation;
			}
			set {
				_rotation = value;
				if (InstanceObject != null)
					Transform.rotation = _rotation;
			}
		}
		/// <summary>
		/// Размер объекта
		/// </summary>
		public Vector3 Scale
		{
			get {
				if (InstanceObject != null)
					_scale = Transform.localScale;
				return _scale;
			}
			set {
				_scale = value;
				if (InstanceObject != null)
					Transform.localScale = _scale;
			}
		}
		/// <summary>
		/// Имя объекта
		/// </summary>
		public string Name
		{
			get { return _name; }
			set {
				_name = value;
				if(InstanceObject != null)
					InstanceObject.name = _name;
			}
		}
		/// <summary>
		/// Слой объекта
		/// </summary>
		public int Layer
		{
			get { return _layer; }
			set {
				_layer = value;
				if (_instanceObject != null) {
					_instanceObject.layer = _layer;
					SetLayer(Transform, value);
				}
			}
		}
		/// <summary>
		/// Материал объекта
		/// </summary>
		public Material Material { get { return _material; } }
		/// <summary>
		/// Цвет материала объекта
		/// </summary>
		public Color Color
		{
			get { return _color; }
			set {
				_color = value;
				if (_material != null)
					_material.color = _color;
				SetColor(Transform, value);
			}
		}
		/// <summary>
		/// Физическое свойство объекта
		/// </summary>
		public Rigidbody Rigidbody { get { return _rigidbody; } }
		/// <summary>
		/// Коллайдер объекта
		/// </summary>
		public Collider Collider { get { return _collider; } }
		/// <summary>
		/// Скрывает/показывает объект
		/// </summary>
		public bool IsVisible
		{
			get { return _isVisible; }
			set {
				_isVisible = value;
				SetVisible(Transform, _isVisible);
			}
		}
		/// <summary>
		/// Компонент, отвечающий за воспроизведение аудио
		/// </summary>
		public AudioSource AudioSource { get { return _audioSource; } }
		/// <summary>
		/// Компонент, отвечающий за воспроизведение системы частиц
		/// </summary>
		public ParticleSystem ParticleSystem { get { return _particleSystem; } }
		/// <summary>
		/// Компонент, отвечающий за анимацию объекта
		/// </summary>
		public Animator Animator { get { return _animator; } }
		#endregion

		#region PrivateFunction
		/// <summary>
		/// Выставляет слой себе и всем вложенным объектам независимо от уровня вложенности
		/// </summary>
		private void SetLayer(Transform obj, int layer) {
			obj.gameObject.layer = layer; // Выставляем объекту слой
			if (obj.childCount > 0) {
				foreach (Transform child in obj) // Проходит по всем вложенным объектам
					SetLayer(child, layer); // Рекурсивный вызов функции
			}
		}
		/// <summary>
		/// Выставляет цвет себе и всем вложенным объектам независимо от уровня вложенности
		/// </summary>
		private void SetColor(Transform obj, Color color) {
			var rend = obj.GetComponent<Renderer>();
			if (rend)	// если объект имеет компонент Renderer
				foreach (Material mat in rend.materials)
					mat.color = color;		// присваиваем цвет всем его материалам
			if (obj.childCount == 0) return;	// если имеются дочерние объекты
			foreach (Transform child in obj)
				SetColor(child, color);	// рекурсивно вызываем для них эту функцию
		}
		/// <summary>
		/// Показывает/скрывает себя и всех вложенных объектов независимо от уровня вложенности
		/// </summary>
		private void SetVisible(Transform obj, bool isVisible) {
			MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
			if (meshRenderer) meshRenderer.enabled = isVisible;
			SkinnedMeshRenderer skinRenderer = obj.GetComponent<SkinnedMeshRenderer>();
			if (skinRenderer) skinRenderer.enabled = isVisible;
			if (obj.childCount > 0) {
				foreach (Transform child in obj) // Проходит по всем вложенным объектам
					SetVisible(child, isVisible); // Рекурсивный вызов функции
			}
		}
		#endregion
	}
}