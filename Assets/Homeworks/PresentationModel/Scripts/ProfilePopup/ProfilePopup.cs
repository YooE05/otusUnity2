using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public class ProfilePopup : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        [SerializeField] private PlayerLevelView _levelView;
        [SerializeField] private UserInfoView _userInfoView;

        [SerializeField] private Transform _statsContainer;
        [SerializeField] private CharacterStatView _statViewPrefab;
        private readonly List<CharacterStatView> _statsViews = new List<CharacterStatView>();

        private ProfilePresenter _presenter;

        public void Show(IPresenter args)
        {
            if (args is not ProfilePresenter)
            {
                throw new InvalidOperationException("Expected Profile Presenter");
            }

            gameObject.SetActive(true);
            _presenter = (ProfilePresenter) args;
            _closeButton.onClick.AddListener(Hide);

            _userInfoView.Init(_presenter.UserInfoPresenter);
            _levelView.Init(_presenter.LevelPresenter);

            foreach (var presenter in _presenter.StatPresenters)
            {
                CreateStatView(presenter);
            }

            _presenter.OnStatPresenterAdded += CreateStatView;
        }

        private void CreateStatView(CharacterStatPresenter statPresenter)
        {
            CharacterStatView stat = Instantiate(_statViewPrefab, _statsContainer);
            stat.Init(statPresenter);
            _statsViews.Add(stat);
        }

        public void Hide()
        {
            if (_presenter != null)
            {
                _presenter.OnStatPresenterAdded -= CreateStatView;
            }

            for (var i = _statsViews.Count - 1; i >= 0; i--)
            {
                CharacterStatView productView = _statsViews[i];
                Destroy(productView.gameObject);
            }

            _statsViews.Clear();

            _closeButton.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }
    }
}