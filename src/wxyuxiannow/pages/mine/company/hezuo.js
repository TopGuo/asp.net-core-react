const Api = require('../../../utils/httpPost');
Page({

  /**
   * 页面的初始数据
   */
  data: {
    dataList: []
  },
  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {
    Api.Post('/api/Announces', {}).then(res => {
      console.log(res)
      if (res.code == 200) {
        this.setData({
          dataList: res.data
        })
      }
    });
  }
})