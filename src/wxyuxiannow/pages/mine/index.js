const app = getApp()
const auth = require("../../utils/auth");
const Api = require('../../utils/httpPost');
Page({

  /**
   * 页面的初始数据
   */
  data: {
    userInfo: false,
    userMobile: '18333103619',
    isVip: false,
    shopId: -1
  },
  onGotUserInfo(e) {
    console.log(e.detail.userInfo)
    if (!e.detail.userInfo) {
      wx.showToast({
        title: '您已取消登录',
        icon: 'none',
      })
      return;
    }
    if (app.globalData.isConnected) {
      wx.setStorageSync('userInfo', e.detail.userInfo)
      auth.login(this);
    } else {
      wx.showToast({
        title: '当前无网络',
        icon: 'none',
      })
    }
  },
  aboutUs: function () {
    wx.showModal({
      title: '关于我们',
      content: '河北星辰无限科技有限公司 \r\n成立于2016年\r\n总部位于河北省石家庄市桥西区想悦天地',
      showCancel: false
    })
  },
  heZuo: function () {
    wx.showModal({
      title: '商务合作',
      content: '河北星辰无限科技有限公司 \r\n客服联系方式\r\n 17336318815',
      showCancel: false
    })
  },
  loginOut: function () {
    auth.loginOut();
    wx.reLaunch({
      url: '/pages/mine/index'
    })
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {
    wx.setNavigationBarTitle({
      title: '我的'
    })
  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {
    const _this = this;
    auth.checkHasLogined().then(isLogined => {
      if (isLogined) {
        _this.setData({
          userInfo: wx.getStorageSync('userInfo')
        })
      }
    })
    Api.Post('/api/CheckUserStatus', {}, 'Get').then(res => {
      if (res.code == 200) {
        this.setData({
          isVip: res.data.status,
          shopId: res.data.id
        })
      }
    });
  }
})