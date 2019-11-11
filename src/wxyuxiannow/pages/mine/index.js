const app = getApp()
Page({

  /**
   * 页面的初始数据
   */
  data: {
    userInfo: false,
    userMobile:'18333103619',
    isVip:true
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
      AUTH.login(this);
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
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {

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

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})