// pages/mine/ShopManager/index.js
const Api = require('../../../utils/httpPost');
Page({

  /**
   * 页面的初始数据
   */
  data: {
    status: 0,
    shopId: -1,
    images: [],
    base64Imgs: '',
    content: ''
  },
  bindAreaBlur: function (e) {
    this.setData({
      content: e.detail.value
    })
  },
  chooseImage: function (e) {
    var selectPictureNum = e.target.dataset.num;
    if (selectPictureNum >= 1) {
      wx.showModal({
        title: '提示',
        content: '单次只能上传一张',
        cancelText: '取消'
      })
      return;
    }
    var that = this;
    wx.chooseImage({
      sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
      sourceType: ['camera','album'], // 可以指定来源是相册还是相机，默认二者都有
      success: function (res) {
        var tempFilePaths = res.tempFilePaths;
        that.setData({
          images: that.data.images.concat(tempFilePaths)
        })
        that.uploadImg(tempFilePaths);
      }
    })

  },
  uploadImg: function (uploadImgs) {
    uploadImgs.map((v) => {
      this.uploadBase64File(v);
    });

  },
  uploadBase64File: function (v) {
    wx.getFileSystemManager().readFile({
      filePath: v, //选择图片返回的相对路径
      encoding: 'base64', //编码格式
      success: (res) => {
        let baseImg = 'data:image/png;base64,' + res.data
        this.setData({
          base64Imgs: baseImg
        })
      }
    })
  },
  pubMessage: function () {
    let that = this;
    wx.showLoading()
    let data = {};
    data.pic = that.data.base64Imgs;
    data.content = that.data.content;
    data.shopId = that.data.shopId;
    Api.Post('/api/PubShopDetail', data).then(res => {
      wx.hideLoading();
      if (res.code == 200) {
        wx.showModal({
          title: '提示',
          content: '提交审核成功',
          confirmText: '确定',
          success(res) {
            if (res.confirm) {
              wx.navigateBack()
            }
          }
        })
      } else {
        wx.showModal({
          title: '提示',
          content: '提交审核失败' + res.message
        })
      }
    })
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    this.setData({
      status: options.isVip,
      shopId: options.shopId
    })
  }
})