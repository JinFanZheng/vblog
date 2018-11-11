import { Component } from 'react';
import { connect } from 'dva';
import { Button } from 'antd';

class Login extends Component {
  render() {
    console.log(this.props);
    return (
      <div>
        <p>Hello World.</p>
        <Button
          onClick={() => {
            this.props.dispatch({ type: 'login/fetch', payload: {} });
          }}
        >
          Click me
        </Button>
      </div>
    );
  }
}

export default connect(({ login }) => ({ login }))(Login);
