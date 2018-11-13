import { Component } from 'react';
import { connect } from 'dva';
import { Row, Col, Form, Input, Button, Select, Icon, Switch, InputNumber, Modal } from 'antd';
import ReactMarkdown from 'react-markdown';
import Link from 'umi/link';
import action from 'utils/action';

const FormItem = Form.Item;
const Option = Select.Option;

class Edit extends Component {
  state = {
    content: '',
    showTagModal: false,
    showCategoryModal: false,
    categoryName: '',
    tagName: '',
    init: false,
  };
  componentDidMount() {
    console.log(this.state);
    if (!this.state.init) {
      this.setState({ content: this.props.article.detail.content, init: true });
    }
  }
  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(
      [
        'title',
        'category',
        'summary',
        'tag',
        'content',
        'isTop',
        'sort',
        'isEnabled',
        'canComment',
      ],
      (err, values) => {
        if (!err) {
          values.tag = values.tag.join(',');
          this.props.dispatch(action('article/save', values));
        }
      }
    );
  };
  tagSubmit = () => {
    this.props.form.validateFields(['tagName'], (err, values) => {
      if (!err) {
        this.setState({
          tagName: values.tagName,
        });
        this.props.dispatch(action('article/insertTag', { name: values.tagName }));
        this.setState({
          showTagModal: false,
        });
      }
    });
  };
  categorySubmit = () => {
    this.props.form.validateFields(['categoryName'], (err, values) => {
      if (!err) {
        this.setState({
          categoryName: values.categoryName,
        });
        this.props.dispatch(action('article/insertCategory', { name: values.categoryName }));
        this.setState({
          showCategoryModal: false,
        });
      }
    });
  };
  render() {
    const { form, article } = this.props;
    const { detail } = article;
    console.log(detail);
    const { getFieldDecorator } = form;
    const categories = article.categories.map(p => (
      <Option value={p.name} key={p.id}>
        {p.name}
      </Option>
    ));
    const tags = article.tags.map(p => (
      <Option value={p.name} key={p.id}>
        {p.name}
      </Option>
    ));
    const formItemLayout = {
      labelCol: { span: 4 },
      wrapperCol: { span: 20 },
    };
    const { insertTagSuccess, insertCategorySuccess } = article;
    return (
      <div style={{ width: 980, margin: '0 auto' }}>
        <Modal
          title="添加分类"
          visible={this.state.showCategoryModal}
          onOk={this.categorySubmit}
          onCancel={() => {
            this.setState({
              showCategoryModal: false,
            });
          }}
          okText="确认"
          cancelText="取消"
        >
          <FormItem {...formItemLayout} label="分类名称">
            {getFieldDecorator('categoryName', {
              rules: [
                {
                  required: true,
                  message: '请输入分类名称.',
                },
              ],
            })(<Input placeholder="请输入分类名称" />)}
          </FormItem>
        </Modal>
        <Modal
          title="添加标签"
          visible={this.state.showTagModal}
          onOk={this.tagSubmit}
          onCancel={() => {
            this.setState({
              showTagModal: false,
            });
          }}
          okText="确认"
          cancelText="取消"
        >
          <FormItem {...formItemLayout} label="标签名称">
            {getFieldDecorator('tagName', {
              rules: [
                {
                  required: true,
                  message: '请输入标签名称.',
                },
              ],
            })(<Input placeholder="请输入标签名称" />)}
          </FormItem>
        </Modal>
        <Form layout="vertical" onSubmit={this.handleSubmit}>
          <FormItem label="　">
            <Button style={{ marginRight: 15 }} type="primary" htmlType="submit">
              发表文章
            </Button>
            <Link to={'/article'}>
              <Button>返回列表</Button>
            </Link>
          </FormItem>
          <Row gutter={18}>
            <Col span={16}>
              <FormItem label="文章标题">
                {getFieldDecorator('title', {
                  rules: [{ required: true, message: '请输入文章标题.' }],
                  initialValue: detail.title,
                })(<Input placeholder={'文章标题，字数控制在 100 个字以内'} />)}
              </FormItem>
            </Col>
            <Col span={8}>
              <Row gutter={5}>
                <Col span={20}>
                  <FormItem label="分类">
                    {getFieldDecorator('category', {
                      rules: [{ required: true, message: '请选择分类.' }],
                      initialValue: detail.category,
                    })(
                      <Select
                        showSearch
                        placeholder="关键字"
                        optionFilterProp="children"
                        onChange={() => {}}
                        onFocus={() => {}}
                        onBlur={() => {}}
                        filterOption={(input, option) =>
                          option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0
                        }
                      >
                        {categories}
                      </Select>
                    )}
                  </FormItem>
                </Col>
                <Col span={4}>
                  <FormItem label="　">
                    <Button
                      onClick={() => {
                        this.setState({
                          showCategoryModal: true,
                        });
                      }}
                    >
                      <Icon type={'plus'} />
                    </Button>
                  </FormItem>
                </Col>
              </Row>
            </Col>
          </Row>
          <FormItem label="摘要">
            {getFieldDecorator('summary', {
              rules: [{ required: true, message: '摘要不能为空.' }],
              initialValue: detail.summary,
            })(
              <Input.TextArea
                autosize={true}
                placeholder={'这里输入文章摘要...'}
                onChange={e => {
                  this.setState({
                    summary: e.target.value,
                  });
                }}
              />
            )}
          </FormItem>
          <Row gutter={18}>
            <Col span={10}>
              <Row gutter={5}>
                <Col span={20}>
                  <FormItem label="标签">
                    {getFieldDecorator('tag', {
                      rules: [{ required: true, message: '请选择标签.' }],
                      initialValue: detail.tag.split(','),
                    })(
                      <Select
                        mode="multiple"
                        style={{ width: '100%' }}
                        placeholder="请选择标签."
                        onChange={() => {}}
                      >
                        {tags}
                      </Select>
                    )}
                  </FormItem>
                </Col>
                <Col span={4}>
                  <FormItem label="　">
                    <Button
                      onClick={() => {
                        this.setState({
                          showTagModal: true,
                        });
                      }}
                    >
                      <Icon type={'plus'} />
                    </Button>
                  </FormItem>
                </Col>
              </Row>
            </Col>
            <Col span={3}>
              <FormItem label="是否置顶">
                {getFieldDecorator('isTop', {
                  initialValue: detail.isTop,
                })(
                  <Switch
                    checkedChildren={<Icon type="check" />}
                    unCheckedChildren={<Icon type="close" />}
                    onChange={() => {}}
                    // checked={detail.isTop}
                  />
                )}
              </FormItem>
            </Col>
            <Col span={3}>
              <FormItem label="允许评论">
                {getFieldDecorator('canComment', {
                  initialValue: true,
                })(
                  <Switch
                    checkedChildren={<Icon type="check" />}
                    unCheckedChildren={<Icon type="close" />}
                    onChange={() => {}}
                  />
                )}
              </FormItem>
            </Col>
            <Col span={3}>
              <FormItem label="是否启用">
                {getFieldDecorator('isEnabled', {
                  initialValue: true,
                })(
                  <Switch
                    checkedChildren={<Icon type="check" />}
                    unCheckedChildren={<Icon type="close" />}
                    onChange={() => {}}
                  />
                )}
              </FormItem>
            </Col>
            <Col span={5}>
              <FormItem label="排序">
                {getFieldDecorator('sort', {
                  rules: [{ required: true, message: '排序值.' }],
                  initialValue: detail.sort,
                })(<InputNumber min={1} max={10} onChange={() => {}} />)}
              </FormItem>
            </Col>
          </Row>
          <Row gutter={18}>
            <Col span={12}>
              <FormItem label="正文">
                {getFieldDecorator('content', {
                  rules: [{ required: true, message: '正文不能为空.' }],
                  initialValue: detail.content,
                })(
                  <Input.TextArea
                    autosize={true}
                    placeholder={'这里输入文章内容...'}
                    onChange={e => {
                      this.setState({
                        content: e.target.value,
                      });
                    }}
                  />
                )}
              </FormItem>
            </Col>
            <Col span={12}>
              <ReactMarkdown source={this.state.content} escapeHtml={false} />
            </Col>
          </Row>
        </Form>
      </div>
    );
  }
}

export default connect(({ article }) => ({ article }))(Form.create()(Edit));
