﻿//  // Copyright (c) Microsoft.  All rights reserved.// //  Licensed under the Apache License, Version 2.0 (the "License");//  you may not use this file except in compliance with the License.//  You may obtain a copy of the License at//    http://www.apache.org/licenses/LICENSE-2.0// //  Unless required by applicable law or agreed to in writing, software//  distributed under the License is distributed on an "AS IS" BASIS,//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.//  See the License for the specific language governing permissions and//  limitations under the License.namespace Microsoft.Azure.Commands.ApiManagement.ServiceManagement.Commands{    using System;    using System.Management.Automation;    using Microsoft.Azure.Commands.ApiManagement.ServiceManagement.Models;    [Cmdlet(VerbsCommon.Add, "AzureApiManagementProductToGroup")]    [OutputType(typeof(bool))]    public class AddAzureApiManagementProductToGroup : AzureApiManagementCmdletBase    {        [Parameter(            ValueFromPipelineByPropertyName = true,             Mandatory = true,             HelpMessage = "Instance of PsApiManagementContext. This parameter is required.")]        [ValidateNotNullOrEmpty]        public PsApiManagementContext Context { get; set; }        [Parameter(            ValueFromPipelineByPropertyName = true,             Mandatory = true,             HelpMessage = "Identifier of existing group. This parameter is required.")]        [ValidateNotNullOrEmpty]        public String GroupId { get; set; }        [Parameter(            ValueFromPipelineByPropertyName = true,             Mandatory = true,             HelpMessage = "Identifier of existing product. This parameter is required.")]        [ValidateNotNullOrEmpty]        public String ProductId { get; set; }        [Parameter(            ValueFromPipelineByPropertyName = true,             Mandatory = false,             HelpMessage = "If specified will write true in case operation succeeds. This parameter is optional. Default value is false.")]        public SwitchParameter PassThru { get; set; }        public override void ExecuteApiManagementCmdlet()        {            Client.ProductAddToGroup(Context, GroupId, ProductId);            if (PassThru)            {                WriteObject(true);            }        }    }} r   i s   r e q u i r e d . " ) ]                  [ V a l i d a t e N o t N u l l O r E m p t y ]                  p u b l i c   P s A p i M a n a g e m e n t C o n t e x t   C o n t e x t   {   g e t ;   s e t ;   }                   [ P a r a m e t e r (                          V a l u e F r o m P i p e l i n e B y P r o p e r t y N a m e   =   t r u e ,                            M a n d a t o r y   =   t r u e ,                            H e l p M e s s a g e   =   " I d e n t i f i e r   o f   e x i s t i n g   g r o u p .   T h i s   p a r a m e t e r   i s   r e q u i r e d . " ) ]                  [ V a l i d a t e N o t N u l l O r E m p t y ]                  p u b l i c   S t r i n g   G r o u p I d   {   g e t ;   s e t ;   }                   [ P a r a m e t e r (                          V a l u e F r o m P i p e l i n e B y P r o p e r t y N a m e   =   t r u e ,                            M a n d a t o r y   =   t r u e ,                            H e l p M e s s a g e   =   " I d e n t i f i e r   o f   e x i s t i n g   p r o d u c t .   T h i s   p a r a m e t e r   i s   r e q u i r e d . " ) ]                  [ V a l i d a t e N o t N u l l O r E m p t y ]                  p u b l i c   S t r i n g   P r o d u c t I d   {   g e t ;   s e t ;   }                   [ P a r a m e t e r (                          V a l u e F r o m P i p e l i n e B y P r o p e r t y N a m e   =   t r u e ,                            M a n d a t o r y   =   f a l s e ,                            H e l p M e s s a g e   =   " I f   s p e c i f i e d   w i l l   w r i t e   t r u e   i n   c a s e   o p e r a t i o n   s u c c e e d s .   T h i s   p a r a m e t e r   i s   o p t i o n a l .   D e f a u l t   v a l u e   i s   f a l s e . " ) ]                  p u b l i c   S w i t c h P a r a m e t e r   P a s s T h r u   {   g e t ;   s e t ;   }                   p u b l i c   o v e r r i d e   v o i d   E x e c u t e A p i M a n a g e m e n t C m d l e t ( )                  {                          C l i e n t . P r o d u c t A d d T o G r o u p ( C o n t e x t ,   G r o u p I d ,   P r o d u c t I d ) ;                           i f   ( P a s s T h r u )                          {                                  W r i t e O b j e c t ( t r u e ) ;                          }                  }          }  } 