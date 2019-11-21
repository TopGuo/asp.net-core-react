// pages/nearShop/notice.js
const WxParse = require('../../wxParse/wxParse.js');
const Api = require('../../utils/httpPost');
Page({

  /**
   * 页面的初始数据
   */
  data: {
    notice: { title: "跟我左边画条龙" },
    article: ''
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    console.log(options)
    Api.Post('/api/OneAnnounces', { id: options.id }).then(res => {
      console.log(res)
      this.setData({
        article: res.data.content,
        notice: {
          title: res.data.createTime
        }
      });
      WxParse.wxParse('article', 'html', this.data.article, this, 5);
    });
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

  }
})