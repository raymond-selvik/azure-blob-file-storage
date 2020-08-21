import React, { Component } from 'react';
import { BiFolder } from "react-icons/bi";

export class DirItem extends Component {

    constructor(props) {
        super(props);
        console.log(this.props.dir.name);
        this.state = {
            dir: this.props.dir
        } 
    }
    render() {
        return (
            <tr key={this.state.dir.name}>
                <td><BiFolder/>{this.state.dir.name}</td>
                <td>{this.state.dir.fullPath}</td>
            </tr>
        );
    }
}
