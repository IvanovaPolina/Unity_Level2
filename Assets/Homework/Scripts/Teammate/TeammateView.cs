namespace Homework
{
	public sealed class TeammateView : BaseSceneObject
	{
		private TeammateModel teammateModel;

		protected override void Awake() {
			base.Awake();
			teammateModel = GetComponentInParent<TeammateModel>();
			TeammateController.OnTeammateSelected += TeammateSelected;
			IsVisible = false;
		}

		private void OnDestroy() {
			TeammateController.OnTeammateSelected -= TeammateSelected;
		}

		private void TeammateSelected(TeammateModel teammate) {
			IsVisible = teammateModel == teammate;
		}
	}
}