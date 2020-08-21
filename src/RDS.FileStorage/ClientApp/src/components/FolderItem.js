import React, { Component } from 'react';
import { BiFolder } from "react-icons/bi";

export class FolderItem extends Component {

    constructor(props) {
        super(props);
        console.log(this.props.folder.name);
        this.state = {
            folder: this.props.folder
        } 
    }
    render() {
        return (
            <tr key={this.state.folder.name} > 
                <td><BiFolder/>{this.state.folder.name}</td>
                <td>{this.state.folder.fullPath}</td>
            </tr>
        );
    }
}
