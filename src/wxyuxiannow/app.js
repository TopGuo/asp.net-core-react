
App({
  onLaunch: function () {
    const that = this;
    wx.getSystemInfo({
      success(res) {
        that.systemInfo = res;
      },
    });
    /**
     * 初次加载判断网络情况
     * 无网络状态下根据实际情况进行调整
     */
    wx.getNetworkType({
      success(res) {
        const networkType = res.networkType
        if (networkType === 'none') {
          that.globalData.isConnected = false
          wx.showToast({
            title: '当前无网络',
            icon: 'loading',
            duration: 2000
          })
        }
      }
    });
    /**
     * 监听网络状态变化
     * 可根据业务需求进行调整
     */
    wx.onNetworkStatusChange(function (res) {
      if (!res.isConnected) {
        that.globalData.isConnected = false
        wx.showToast({
          title: '网络已断开',
          icon: 'loading',
          duration: 2000,
          complete: function () {
            that.goStartIndexPage()
          }
        })
      } else {
        that.globalData.isConnected = true
        wx.hideToast()
      }
    });
  },
  onShow: function (e) {
    this.globalData.launchOptions = e;
    if (e && e.query && e.query.invite_id) {
      wx.setStorageSync('refId', e.query.invite_id);
    }
  },
  globalData: {
    userInfo: null,
    isConnected: true,
    launchOptions: null
  },
  systemInfo: null,
})