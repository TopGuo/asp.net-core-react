const Api = require('./httpPost');

function login(page) {
  wx.login({
    success: function(res) {
      Api.Post("/api/WxLogin", {
        code: res.code
      }).then(res => {
        if (res.code == 10005) {
          register(page);
        }
        if (res.code != 200) {
          wx.showModal({
            title: '错误',
            content: '无法登陆请重试' + res.message,
            showCancel: false
          });
          return;
        }
        wx.setStorageSync('token', res.data.token);
        wx.setStorageSync('uid', res.data.uid);
        if (page) {
          page.onShow();
        }
      })
    }
  });
}

function register(page) {
  wx.login({
    success: function(res) {
      let code = res.code;
      wx.getUserInfo({
        success: function(res) {
          let iv = res.iv;
          let encryptedData = res.encryptedData;
          let referrer = '' // 推荐人
          let referrer_storge = wx.getStorageSync('refId');
          if (referrer_storge) {
            referrer = referrer_storge;
          }
          Api.Post("/api/Register", {
            code: code,
            encryptedData: encryptedData,
            iv: iv,
            referrer: referrer
          }).then(res => {
            login(page);
          })
        }
      })
    }
  })
}

function loginOut() {
  wx.removeStorageSync('token');
  wx.removeStorageSync('userInfo');
}

function checkHasLogined() {
  return new Promise((resolve, reject) => {
    const token = wx.getStorageSync('token')
    if (!token) {
      resolve(false);
    }
    wx.checkSession({
      fail() {
        resolve(false);
      }
    })
    const checkTokenRes = Api.Post('/api/CheckToken', {}, 'Get').then(res => {
      if (res.data) {
        resolve(true);
      } else {
        wx.removeStorageSync('token')
        resolve(false);
      }
    })
  });
}
module.exports = {
  login: login,
  register: register,
  loginOut: loginOut,
  checkHasLogined: checkHasLogined
}