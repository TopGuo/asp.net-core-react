// pages/scenic/trip/trip.js
const WxParse = require('../../../wxParse/wxParse.js');
const constants = require('../../../configs/constants.js');
const Api = require('../../../utils/httpPost');
Page({

  /**
   * 页面的初始数据
   */
  data: {
    notice: {
      title: "..."
    }
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    let id = options.id;
    Api.Post('/api/OneScenic', { id: id }).then(res => {
      if (res.code == 200) {
        this.setData({
          notice: {
            title: res.data.createTime
          }
        })
        wx.setNavigationBarTitle({
          title: res.data.title
        })
        WxParse.wxParse('trip', 'html', res.data.content, this, 5);
      }
    })
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

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
    return {
      title: constants.shareProfile,
      path: '/pages/message/index?inviter_id=' + wx.getStorageSync('uid'),
      success: function (res) {
        // 转发成功
      },
      fail: function (res) {
        // 转发失败
      }
    }
  }
})